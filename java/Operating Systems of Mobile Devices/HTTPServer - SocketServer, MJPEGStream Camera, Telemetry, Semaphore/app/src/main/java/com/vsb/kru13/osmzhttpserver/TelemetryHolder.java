package com.vsb.kru13.osmzhttpserver;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;

import androidx.core.app.ActivityCompat;

import org.json.JSONArray;
import org.json.JSONObject;

import java.sql.Timestamp;
import java.util.ArrayList;

public class TelemetryHolder implements LocationListener, SensorEventListener {

    private static TelemetryHolder instance = null;

    private static Context context;
    private static LocationManager locationManager;
    private static SensorManager sensorManager;

    private boolean locationReady = false;
    private double locationLon = 0;
    private double locationLat = 0;
    private double locationAlt = 0;

    private boolean accelerometerReady = false;
    private float accelerometerX = 0;
    private float accelerometerY = 0;
    private float accelerometerZ = 0;

    private boolean gyroscopeReady = false;
    private float gyroscopeX = 0;
    private float gyroscopeY = 0;
    private float gyroscopeZ = 0;

    private boolean magneticFieldReady = false;
    private float magneticFieldX = 0;
    private float magneticFieldY = 0;
    private float magneticFieldZ = 0;

    private boolean pressureReady = false;
    private float pressure = 0;

    public TelemetryHolder(Context ctx, LocationManager lm, SensorManager sm) {
        context = ctx;

        // Location
        locationManager = lm;
        if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            locationManager.requestLocationUpdates(locationManager.GPS_PROVIDER, 0, 0, this);
            locationManager.requestLocationUpdates(locationManager.NETWORK_PROVIDER, 0, 0, this);
        }

        // Sensors
        sensorManager = sm;
        if(sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER) != null){
            accelerometerReady = true;
            sensorManager.registerListener(this, sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER), SensorManager.SENSOR_DELAY_NORMAL);
        }
        if(sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE) != null){
            gyroscopeReady = true;
            sensorManager.registerListener(this, sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE), SensorManager.SENSOR_DELAY_NORMAL);
        }
        if(sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD) != null){
            magneticFieldReady = true;
            sensorManager.registerListener(this, sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD), SensorManager.SENSOR_DELAY_NORMAL);
        }
        if(sensorManager.getDefaultSensor(Sensor.TYPE_PRESSURE) != null){
            pressureReady = true;
            sensorManager.registerListener(this, sensorManager.getDefaultSensor(Sensor.TYPE_PRESSURE), SensorManager.SENSOR_DELAY_NORMAL);
        }
    }

    public static TelemetryHolder getInstance() {
        if(instance == null) {
            instance = new TelemetryHolder(context, locationManager, sensorManager);
        }
        return instance;
    }

    public JSONObject getJSON() {
        try {
            JSONObject obj = new JSONObject();
            if(locationReady) {obj.put("location", new JSONArray(getLocation()));}
            if(accelerometerReady) {obj.put("accelerometer", new JSONArray(getAccelerometer()));}
            if(gyroscopeReady) {obj.put("gyroscope", new JSONArray(getGyroscope()));}
            if(magneticFieldReady) {obj.put("magnetic_field", new JSONArray(getMagneticField()));}
            if(pressureReady) {obj.put("pressure", getPressure());}
            obj.put("timestamp", new Timestamp(System.currentTimeMillis()));
            return obj;
        } catch (Throwable e) {
            e.printStackTrace();
        }
        return null;
    }

    private ArrayList<Double> getLocation() {
        ArrayList<Double> location = new ArrayList<>();
        location.add(locationLon);
        location.add(locationLat);
        location.add(locationAlt);
        return location;
    }

    private ArrayList<Float> getAccelerometer() {
        ArrayList<Float> accelerometer = new ArrayList<>();
        accelerometer.add(accelerometerX);
        accelerometer.add(accelerometerY);
        accelerometer.add(accelerometerZ);
        return accelerometer;
    }

    private ArrayList<Float> getGyroscope() {
        ArrayList<Float> gyroscope = new ArrayList<>();
        gyroscope.add(gyroscopeX);
        gyroscope.add(gyroscopeY);
        gyroscope.add(gyroscopeZ);
        return gyroscope;
    }

    private ArrayList<Float> getMagneticField() {
        ArrayList<Float> magneticField = new ArrayList<>();
        magneticField.add(magneticFieldX);
        magneticField.add(magneticFieldY);
        magneticField.add(magneticFieldZ);
        return magneticField;
    }

    private Float getPressure() {
        return pressure;
    }

    @Override
    public void onLocationChanged(Location location) {
        locationReady = true;
        locationLon = location.getLongitude();
        locationLat = location.getLatitude();
        locationAlt = location.getAltitude();
    }

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {
        locationReady = true;
    }

    @Override
    public void onProviderDisabled(String s) {
        locationReady = false;
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        switch(sensorEvent.sensor.getType()) {
            case Sensor.TYPE_ACCELEROMETER:
                accelerometerX = sensorEvent.values[0];
                accelerometerY = sensorEvent.values[1];
                accelerometerZ = sensorEvent.values[2];
                break;
            case Sensor.TYPE_GYROSCOPE:
                gyroscopeX = sensorEvent.values[0];
                gyroscopeY = sensorEvent.values[1];
                gyroscopeZ = sensorEvent.values[2];
                break;
            case Sensor.TYPE_MAGNETIC_FIELD:
                magneticFieldX = sensorEvent.values[0];
                magneticFieldY = sensorEvent.values[1];
                magneticFieldZ = sensorEvent.values[2];
                break;
            case Sensor.TYPE_PRESSURE:
                pressure = sensorEvent.values[0];
                break;
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }
}
