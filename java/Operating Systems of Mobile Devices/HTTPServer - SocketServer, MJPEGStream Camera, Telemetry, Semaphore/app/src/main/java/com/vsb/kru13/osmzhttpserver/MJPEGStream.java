package com.vsb.kru13.osmzhttpserver;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class MJPEGStream {
    private OutputStream out;
    private final Handler handler;

    private final int dataChunkSize = 1024;

    MJPEGStream(OutputStream out, Handler handler) {
        this.out = out;
        this.handler = handler;
    }

    private byte[] getHeader(int length) {
        String header = "Content-Type: image/jpeg\r\nContent-Length: " + length + "\r\n\r\n";
        return header.getBytes();
    }

    private byte[] getFooter() {
        String footer = "\r\n--OSMZ_boundary\r\n";
        return footer.getBytes();
    }

    private List<byte[]> loadDataChunks(List<byte[]> dataChunks, byte[] data) {
        int start = 0;
        while(start < data.length) {
            int end = Math.min(data.length, start + dataChunkSize);
            dataChunks.add(Arrays.copyOfRange(data, start, end));
            start += dataChunkSize;
        }
        return dataChunks;
    }

    public void write(final byte[] data) {
        List<byte[]> dataChunks = new ArrayList<byte[]>();

        // Load data chunks
        /*loadDataChunks(dataChunks, data);*/

        // Send picture
        try{
            DataOutputStream stream = new DataOutputStream(out);

            // Header
            stream.write(getHeader(data.length));
            stream.flush();

            // JPEG image
            ByteArrayOutputStream buffer = new ByteArrayOutputStream();
            buffer.reset();
            /*for(byte[] dataChunk : dataChunks) {
                buffer.write(dataChunk, 0, dataChunk.length);
            }*/
            buffer.write(data);
            buffer.flush();
            buffer.writeTo(stream);
            buffer.close();

            // Footer
            stream.write(getFooter());
            stream.flush();

            // Chrome workaround (https://bugs.chromium.org/p/chromium/issues/detail?id=249132)
            //stream.write(getFooter());
            //stream.flush();

            Log.d("MJPEG", "JPEG image added to stream (" + data.length + " bytes)");
            Bundle b = new Bundle();
            b.putString("event", "mjpeg_append");
            b.putInt("image_size", data.length);
            sendHandlerMessage(b);
        }
        catch(SocketException e) {
            e.printStackTrace();

            try {
                out.close();
            } catch (IOException ioException) {
                ioException.printStackTrace();
            }
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
