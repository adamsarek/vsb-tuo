package com.vsb.kru13.osmzhttpserver;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

public class Semaphore {
    private Handler handler;
    private int resources;

    public Semaphore(Handler handler, int resources){
        this.handler = handler;
        this.resources = resources;
    }

    public boolean tryAcquire() {
        if(resources > 0) {
            acquire();
            return true;
        }
        return false;
    }

    public void acquire() {
        int resourcesOld = resources;

        if(resources > 0) {
            resources--;
            Log.d("SEMAPHORE", "Acquiring... " + (resources+1) + " -> " + resources);
        }
        else {
            Log.d("SEMAPHORE", "Acquiring not possible.");
        }

        Bundle b = new Bundle();
        b.putString("event", "semaphore_acquire");
        b.putInt("semaphore_resources_old", resourcesOld);
        b.putInt("semaphore_resources_new", resources);
        sendHandlerMessage(b);
    }

    public void release() {
        resources++;

        Log.d("SEMAPHORE", "Releasing... " + (resources-1) + " -> " + resources);
        Bundle b = new Bundle();
        b.putString("event", "semaphore_release");
        b.putInt("semaphore_resources_old", resources-1);
        b.putInt("semaphore_resources_new", resources);
        sendHandlerMessage(b);
    }

    private void sendHandlerMessage(Bundle b) {
        Message msg = handler.obtainMessage();
        msg.setData(b);
        msg.sendToTarget();
    }
}
