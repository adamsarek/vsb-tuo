package com.vsb.kru13.osmzhttpserver;

import android.graphics.Rect;
import android.graphics.YuvImage;
import android.hardware.Camera;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.webkit.MimeTypeMap;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class ClientThread extends Thread {

    private ServerSocket serverSocket;
    private final Socket s;
    private Handler handler;
    private TelemetryHolder telemetryHolder;
    private Semaphore semaphore;
    private Camera camera;
    private CameraPreview cameraPreview;

    private BufferedWriter bw;
    private BufferedReader br;

    public ClientThread(ServerSocket serverSocket, final Socket s, Handler handler, final TelemetryHolder telemetryHolder, final Semaphore semaphore, Camera camera, CameraPreview cameraPreview){
        this.serverSocket = serverSocket;
        this.s = s;
        this.handler = handler;
        this.telemetryHolder = telemetryHolder;
        this.semaphore = semaphore;
        this.camera = camera;
        this.cameraPreview = cameraPreview;

        try {
            this.bw = new BufferedWriter(new OutputStreamWriter(s.getOutputStream()));
            this.br = new BufferedReader(new InputStreamReader(s.getInputStream()));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {
        Log.d("THREAD", "Starting thread " + currentThread().getId());
        Bundle b = new Bundle();
        b.putString("event", "thread_start");
        b.putLong("thread_id", currentThread().getId());
        sendHandlerMessage(b);

        try {
            /*
             * String tmp = in.readLine();
             * out.Write(tmp.toUpperCase());
             * out.flush();
             * */

            // radky nacitaji tak dlouho, dokud neni prazdny radek
            // nactene radky vypiseme do logcatu
            String tmp, tmpPath = "";
            while((tmp = br.readLine()) != null && !(tmp.isEmpty())){
                // parsovat hlavicku
                // nacitat klientem pozadovane umisteni dokumentu
                // GET /dokumenty/neco.html HTTP/1.0
                // ziskat /dokumenty/neco.html
                String[] tmpSplit = tmp.split(" ");
                if(tmpSplit[0].contentEquals("GET")){
                    tmpPath = tmpSplit[1];
                }
                Log.d("SERVER-CLHEADER", tmp);
            }

            // pokusit se najit pozadovany dokument na SD karte
            String pathToSd = Environment.getExternalStorageDirectory().getAbsolutePath();  // /storage/emulated/0
            String pathToHtml = "/Documents/OSMZ" + (tmpPath.contentEquals("/") ? "/index.html" : tmpPath);
            String filePath = pathToSd + pathToHtml;

            // zkusit otevrit soubor filePath
            File f = new File(filePath);
            String mimeType = MimeTypeMap.getSingleton().getMimeTypeFromExtension(filePath.substring(filePath.lastIndexOf('.') + 1));
            if(tmpPath.contentEquals("/streams/telemetry")){
                JSONObject json = telemetryHolder.getJSON();
                String jsonText = String.valueOf(json);

                // slouzi pro zobrazeni telemetrie zarizeni
                Log.d("HTTP", "200 OK");
                b = new Bundle();
                b.putString("event", "http");
                b.putString("http_state", "200 OK");
                sendHandlerMessage(b);
                bw.write("\nHTTP/1.0 200 OK");
                bw.write("\nContent-Type: " + "application/json");   // Content-Type je MIME typ soubor
                bw.write("\nContent-Length: " + jsonText.length() + "\n"); // Content-Length je delka souboru
                bw.write("\n");
                bw.write(jsonText);
                bw.flush();

                close();

                /*
                * TelemetryHeader třída
                * + Permission
                * Singleton, který drží hodnoty senzorů
                * Předávání kontextu přes konstruktor???
                * - Virtual Sensors - Accelerometer, Gyro, Location
                * */
            }
            else if(tmpPath.contentEquals("/streams/camera")){
                // slouzi pro zobrazeni kamery zarizeni
                Log.d("HTTP", "200 OK");
                b = new Bundle();
                b.putString("event", "http");
                b.putString("http_state", "200 OK");
                sendHandlerMessage(b);
                bw.write("\r\nHTTP/1.0 200 OK");
                bw.write("\r\nContent-Type: " + "multipart/x-mixed-replace; boundary=\"OSMZ_boundary\"\r\n");   // Content-Type je MIME typ soubor
                bw.write("\r\n");
                bw.write("--OSMZ_boundary\r\n");
                bw.flush();

                // 1. varianta - 3 FPS pomocí takePicture()
                /*final MJPEGStream mjpegStream = new MJPEGStream(s.getOutputStream(), handler);
                Timer timer = new Timer();
                TimerTask timerTask = new TimerTask() {
                    @Override
                    public void run() {
                        try{
                            camera.takePicture(null, null, new Camera.PictureCallback() {
                                @Override
                                public void onPictureTaken(final byte[] bytes, Camera camera) {
                                    Thread thread = new Thread(new Runnable(){
                                        @Override
                                        public void run() {
                                            mjpegStream.write(bytes);

                                            cameraPreview.refresh();
                                        }
                                    });
                                    thread.start();
                                    try {
                                        thread.join();
                                    } catch (InterruptedException e) {
                                        e.printStackTrace();
                                    }
                                }
                            });
                        } catch(Exception e) {
                            e.printStackTrace();
                        }
                    }
                };
                timer.schedule(timerTask, 0, 1000 / 3);*/

                // 2. varianta - rychlejší díky využití onPreviewFrame()
                camera.setPreviewCallback(new Camera.PreviewCallback() {
                    @Override
                    public void onPreviewFrame(final byte[] bytes, Camera camera) {
                        Thread thread = new Thread(new Runnable(){
                            @Override
                            public void run() {
                                if(serverSocket.isClosed()) {
                                    close();
                                    camera.setPreviewCallback(null);
                                }
                                else if(s.isClosed()) {
                                    camera.setPreviewCallback(null);
                                }
                                else {
                                    MJPEGStream mjpegStream = null;
                                    try {
                                        mjpegStream = new MJPEGStream(s.getOutputStream(), handler);
                                    } catch (IOException e) {
                                        e.printStackTrace();
                                    }

                                    // Convert bytes to JPEG
                                    Camera.Parameters cameraParameters = camera.getParameters();
                                    final int format = cameraParameters.getPreviewFormat();
                                    final int width = cameraParameters.getPreviewSize().width;
                                    final int height = cameraParameters.getPreviewSize().height;

                                    YuvImage image = new YuvImage(bytes, format, width, height, null);
                                    final Rect rect = new Rect(0, 0, width, height);

                                    ByteArrayOutputStream buffer = new ByteArrayOutputStream();
                                    image.compressToJpeg(rect, 100, buffer);
                                    byte[] data = buffer.toByteArray();

                                    mjpegStream.write(data);
                                }
                            }
                        });
                        thread.start();
                        try {
                            thread.join();
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                });

                /*
                 * GET /streams/camera
                 * Získávat sekvenci framů
                 * Camera API verze 1 - pěkná dokumentace
                 * - Inicializace kamery, možnost získat snímky
                 * onPictureTaken(), takePicture(), předává obrázek v rámci pole byte[]
                 * Vyzkoušet kameru v MainActivity - cameraOpen, zobrazení náhledu (layout?)
                 * Data pak posíláme klientovi v podobě obrázku
                 * Motion JPEG, vrací HTTP hlavičku, Content-Type: multipart, Boundary: zarážka (kde končí snímek)
                 * Nikde nebude close, zavírá to client
                 * takePicture() pomocí timeru
                 * HTML ve kterém bude image source - /streams/camera (prohlížeč bude sám updatovat obrázky)
                 * */
            }
            else if(f.exists() && !f.isDirectory()) {
                // pokud soubor existuje, tak server odpoví 200 OK
                Log.d("HTTP", "200 OK");
                b = new Bundle();
                b.putString("event", "http");
                b.putString("http_state", "200 OK");
                sendHandlerMessage(b);
                bw.write("\nHTTP/1.0 200 OK");
                bw.write("\nContent-Type: " + mimeType);   // Content-Type je MIME typ soubor
                bw.write("\nContent-Length: " + f.length() + "\n"); // Content-Length je delka souboru
                bw.write("\n");
                bw.flush();

                // zde do socketu budeme kopirovat obsah pozadovaneho souboru (kopirovani musi byt binarne)
                // read/write je nutny pro prenos binarnich dat, napr obrazku
                // File.read ----> o.write
                DataOutputStream fout = new DataOutputStream(s.getOutputStream());
                FileInputStream fis = new FileInputStream(filePath);

                // Načítání souboru po částech
                byte[] buff = new byte[1024];
                int len;
                while((len = fis.read(buff)) > 0){
                    fout.write(buff, 0, len);
                }

                // Načítání celého souboru rovnou
                /*byte[] fileBytes = new byte[(int) f.length()];
                fis.read(fileBytes);
                fout.write(fileBytes, 0, (int) f.length());*/

                fout.flush();
            }
            else{
                // pokud soubor neexistuje, pak server vrací 40x HTTP odpoved
                Log.d("HTTP", "404 Not Found");
                b = new Bundle();
                b.putString("event", "http");
                b.putString("http_state", "404 Not Found");
                sendHandlerMessage(b);
                bw.write("\nHTTP/1.0 404 Not Found");
                bw.write("\nContent-Type: text/html\n");
                bw.write("\n");
                bw.write("<html><h1>404 Soubor nenalezen</h1><h4>" + filePath + "</h4></html>");
                bw.flush();

                close();
            }
        }
        catch (IOException e){
            e.printStackTrace();
        }
        finally{
            semaphore.release();
            Log.d("THREAD", "Terminating thread " + currentThread().getId());
            b = new Bundle();
            b.putString("event", "thread_terminate");
            b.putLong("thread_id", currentThread().getId());
            sendHandlerMessage(b);
        }
    }

    private void close() {
        try {
            s.close();
            Log.d("SERVER", "Socket Closed");
            Bundle b = new Bundle();
            b.putString("event", "server_socket");
            b.putString("server_socket_state", "Closed");
            sendHandlerMessage(b);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendHandlerMessage(Bundle b) {
        Message msg = handler.obtainMessage();
        msg.setData(b);
        msg.sendToTarget();
    }

}