package com.example.snake;

import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.drawable.Drawable;

public class CanvasButton {
    public final int x;
    public final int y;
    public final int w;
    public final int h;
    private Drawable icon;
    private final Paint paint;
    private boolean touched = false;
    private final Callback callback;

    public CanvasButton(int x, int y, int w, int h, Drawable icon, int color, int strokeWidth, Callback callback) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.icon = icon;
        this.icon.setBounds(x + w / 4, y + h / 4, x + w * 3 / 4, y + h * 3 / 4);
        this.paint = new Paint();
        this.paint.setAntiAlias(true);
        this.paint.setStyle(Paint.Style.STROKE);
        this.paint.setColor(color);
        this.paint.setStrokeWidth(strokeWidth);
        this.callback = callback;
    }

    public void setIcon(Drawable icon) {
        this.icon = icon;
        this.icon.setBounds(x + w / 4, y + h / 4, x + w * 3 / 4, y + h * 3 / 4);
    }

    public boolean isTouched(float tX, float tY) {
        if(tX >= x && tX <= x + w && tY >= y && tY <= y + h) {
            return true;
        }
        return false;
    }

    public boolean touchDown() {
        paint.setStyle(Paint.Style.FILL_AND_STROKE);
        touched = true;
        return true;
    }

    public boolean touchDownReset() {
        paint.setStyle(Paint.Style.STROKE);
        touched = false;
        return true;
    }

    public boolean touchUp() {
        if(touched) {
            touchDownReset();
            callback.invoke();
        }
        return true;
    }

    public void draw(Canvas canvas) {
        canvas.drawRoundRect(x, y, x + w, y + h, w / 4, h / 4, paint);
        icon.draw(canvas);
    }
}
