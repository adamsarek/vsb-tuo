package com.vsb.kru13.osmzhttpserver;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Camera;
import android.hardware.SensorManager;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ListView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    private SocketServer s;
    private static final int READ_EXTERNAL_STORAGE = 1;
    private static final int ACCESS_FINE_LOCATION = 2;
    private static final int CAMERA = 3;
    private static final int INTERNET = 4;
    private static final int MEDIA_TYPE_IMAGE = 1;
    private static final int MEDIA_TYPE_VIDEO = 2;

    Handler handler = new Handler() {
        @Override
        public void handleMessage(@NonNull Message msg) {
            int resourcesOld, resourcesNew, imageSize;
            long threadId;
            String serverState, socketState, httpState;

            switch(msg.getData().getString("event")) {
                case "server":
                    serverState = msg.getData().getString("server_state");
                    addListItem("Server / Socket: " + serverState);
                    break;
                case "server_socket":
                    socketState = msg.getData().getString("server_socket_state");
                    addListItem("Server / Socket: " + socketState);
                    break;
                case "http":
                    httpState = msg.getData().getString("http_state");
                    addListItem("HTTP: " + httpState);
                    break;
                case "semaphore_acquire":
                    resourcesOld = msg.getData().getInt("semaphore_resources_old");
                    resourcesNew = msg.getData().getInt("semaphore_resources_new");
                    addListItem("Semaphore / Acquire: " + resourcesOld + " -> " + resourcesNew);
                    break;
                case "semaphore_release":
                    resourcesOld = msg.getData().getInt("semaphore_resources_old");
                    resourcesNew = msg.getData().getInt("semaphore_resources_new");
                    addListItem("Semaphore / Release: " + resourcesOld + " -> " + resourcesNew);
                    break;
                case "thread_start":
                    threadId = msg.getData().getLong("thread_id");
                    addListItem("THREAD: Starting thread " + threadId);
                    break;
                case "thread_terminate":
                    threadId = msg.getData().getLong("thread_id");
                    addListItem("THREAD: Terminating thread " + threadId);
                    break;
                case "mjpeg_append":
                    imageSize = msg.getData().getInt("image_size");
                    addListItem("MJPEG: JPEG image added to stream (" + imageSize + " bytes)");
                    break;
            }
        }
    };
    LocationManager locationManager;
    SensorManager sensorManager;
    TelemetryHolder telemetryHolder;

    Camera camera;
    CameraPreview cameraPreview;

    ListView listView;
    ArrayList<String> listItems = new ArrayList<String>();
    ArrayAdapter<String> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button btn1 = (Button)findViewById(R.id.button1);
        Button btn2 = (Button)findViewById(R.id.button2);
        listView = (ListView)findViewById(R.id.listView);
        listView.setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, listItems);
        listView.setAdapter(adapter);

        btn1.setOnClickListener(this);
        btn2.setOnClickListener(this);

        locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
    }


    @Override
    public void onClick(View v) {

        if (v.getId() == R.id.button1) {
            checkReadStoragePermission();
        }
        if (v.getId() == R.id.button2) {
            s.close();
            try {
                s.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }


    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        switch (requestCode) {

            case READ_EXTERNAL_STORAGE:
                if ((grantResults.length > 0) && (grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                    checkLocationPermission();
                }
                break;
            case ACCESS_FINE_LOCATION:
                if ((grantResults.length > 0) && (grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                    checkCameraPermission();
                }
                break;
            case CAMERA:
                if ((grantResults.length > 0) && (grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                    checkInternetPermission();
                }
                break;
            case INTERNET:
                if ((grantResults.length > 0) && (grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                    startSocketServer();
                }
                break;

            default:
                break;
        }
    }

    public void checkReadStoragePermission() {
        int readStoragePermissionCheck = ContextCompat.checkSelfPermission(this, Manifest.permission.READ_EXTERNAL_STORAGE);

        if (readStoragePermissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(
                    this, new String[]{Manifest.permission.READ_EXTERNAL_STORAGE}, READ_EXTERNAL_STORAGE);
        }
        else {
            checkLocationPermission();
        }
    }

    public void checkLocationPermission() {
        int locationPermissionCheck = ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION);

        if(locationPermissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(
                    this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, ACCESS_FINE_LOCATION);
        }
        else {
            checkCameraPermission();
        }
    }

    public void checkCameraPermission() {
        int cameraPermissionCheck = ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA);

        if (cameraPermissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(
                    this, new String[]{Manifest.permission.CAMERA}, CAMERA);
        }
        else {
            checkInternetPermission();
        }
    }

    public void checkInternetPermission() {
        int internetPermissionCheck = ContextCompat.checkSelfPermission(this, Manifest.permission.INTERNET);

        if (internetPermissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(
                    this, new String[]{Manifest.permission.INTERNET}, INTERNET);
        }
        else {
            startSocketServer();
        }
    }

    public void startSocketServer() {
        // Camera
        if(checkCameraHardware(this)) {
            camera = getCameraInstance();

            cameraPreview = new CameraPreview(this, camera);
            FrameLayout preview = (FrameLayout) findViewById(R.id.cameraPreview);
            preview.addView(cameraPreview);
        }

        // Server
        telemetryHolder = new TelemetryHolder(this, locationManager, sensorManager);
        s = new SocketServer(handler, telemetryHolder, camera, cameraPreview);
        s.start();
    }

    /** Check if this device has a camera */
    private boolean checkCameraHardware(Context context) {
        if (context.getPackageManager().hasSystemFeature(PackageManager.FEATURE_CAMERA)){
            // this device has a camera
            return true;
        } else {
            // no camera on this device
            return false;
        }
    }

    /** A safe way to get an instance of the Camera object. */
    public static Camera getCameraInstance(){
        Camera c = null;
        try {
            c = Camera.open(); // attempt to get a Camera instance
        }
        catch (Exception e){
            // Camera is not available (in use or does not exist)
            e.printStackTrace();
        }
        return c; // returns null if camera is unavailable
    }

    public void addListItem(String item) {
        listItems.add(item);
        adapter.notifyDataSetChanged();
    }
}
