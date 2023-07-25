package com.vsb.kru13.osmzhttpserver;

import android.hardware.Camera;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class SocketServer extends Thread {

    ServerSocket serverSocket;
    public final int port = 12345;
    boolean bRunning;

    private Handler handler;
    private TelemetryHolder telemetryHolder;
    private Semaphore semaphore;
    private Camera camera;
    private CameraPreview cameraPreview;

    public SocketServer(Handler handler, TelemetryHolder telemetryHolder, Camera camera, CameraPreview cameraPreview) {
        this.handler = handler;
        this.telemetryHolder = telemetryHolder;
        this.semaphore = new Semaphore(this.handler, 3);
        this.camera = camera;
        this.cameraPreview = cameraPreview;
    }

    public void close() {
        try {
            serverSocket.close();
        } catch (IOException e) {
            Log.d("SERVER", "Error, probably interrupted in accept(), see log");
            Bundle b = new Bundle();
            b.putString("event", "server");
            b.putString("server_state", "Error, probably interrupted in accept(), see log");
            sendHandlerMessage(b);
            e.printStackTrace();
        }
        bRunning = false;
    }

    public void run() {
        try {
            Log.d("SERVER", "Creating Socket");
            Bundle b = new Bundle();
            b.putString("event", "server_socket");
            b.putString("server_socket_state", "Creating");
            sendHandlerMessage(b);
            serverSocket = new ServerSocket(port);
            bRunning = true;

            while (bRunning) {
                Log.d("SERVER", "Socket Waiting for connection");
                b = new Bundle();
                b.putString("event", "server_socket");
                b.putString("server_socket_state", "Waiting for connection");
                sendHandlerMessage(b);
                Socket s = serverSocket.accept();
                Log.d("SERVER", "Socket Accepted");
                b = new Bundle();
                b.putString("event", "server_socket");
                b.putString("server_socket_state", "Accepted");
                sendHandlerMessage(b);

                // - true -> vytvoří nového klienta
                // - false -> vypíše hlášku HTTP 503 Server too busy
                if(semaphore.tryAcquire()) {
                    Thread t = new ClientThread(serverSocket, s, handler, telemetryHolder, semaphore, camera, cameraPreview);
                    t.start();
                }
                else {
                    try {
                        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(s.getOutputStream()));
                        BufferedReader br = new BufferedReader(new InputStreamReader(s.getInputStream()));
                        Log.d("HTTP", "503 Service Unavailable");
                        b = new Bundle();
                        b.putString("event", "http");
                        b.putString("http_state", "503 Service Unavailable");
                        sendHandlerMessage(b);
                        bw.write("\nHTTP/1.0 503 Service Unavailable");
                        bw.write("\nContent-Type: text/html\n");
                        bw.write("\n");
                        bw.write("<html><h1>503 Server je zaneprazdnen</h1></html>");
                        bw.flush();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }

                /*
                 * 1 - více vláken pro více klientů
                 *     - socket accept -> vlákno new ClientThread v něm celé HTTP i close server socketu
                 * 2 - ochrana proti DOS utoku
                 *     - limitovat pocet aktualne bezicich vlaken (napr 3)
                 *     - semafor - promenna ktera rika kolik zdroju mame (binarni semafor má jeden zdroj)
                 *       - acquire(), release() - volání synchronizované
                 *         -1         +1
                 *         pokud je resource == 0, pak se na acquire zablokuje
                 *
                 * pred spustenim vlakna dame acquire (nebo tryAcquire?? a hlášku server too busy bez využití vláken)
                 * release uvolnuji samy vlakna i pokud skonci neuspechem (try-catch-FINALLY)
                 *
                 *       - bool tryAcquire() - pokud je resource == 0 pak vrátí false, jinak se chová jak acquire()
                 * */
            }
        }
        catch (IOException e) {
            if (serverSocket != null && serverSocket.isClosed()){
                Log.d("SERVER", "Normal exit");
                Bundle b = new Bundle();
                b.putString("event", "server");
                b.putString("server_state", "Normal exit");
                sendHandlerMessage(b);
            }
            else {
                Log.d("SERVER", "Error");
                Bundle b = new Bundle();
                b.putString("event", "server");
                b.putString("server_state", "Error");
                sendHandlerMessage(b);
                e.printStackTrace();
            }
        }
        finally {
            serverSocket = null;
            bRunning = false;
        }
    }

    private void sendHandlerMessage(Bundle b) {
        Message msg = handler.obtainMessage();
        msg.setData(b);
        msg.sendToTarget();
    }

}