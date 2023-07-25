package com.example.snake;

import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Rect;

public class CanvasText {
    public final int x;
    public final int y;
    public String text;
    private final Paint paint;
    private final Rect textBounds;

    public CanvasText(int x, int y, String text, int color, Paint.Align textAlign, int textSize) {
        this.x = x;
        this.y = y;
        this.text = text;
        this.textBounds = new Rect();
        this.paint = new Paint();
        this.paint.setAntiAlias(true);
        this.paint.setFakeBoldText(true);
        this.paint.setStyle(Paint.Style.FILL);
        this.paint.setColor(color);
        this.paint.setTextAlign(textAlign);
        this.paint.setTextSize(textSize);
        this.paint.getTextBounds(text, 0, text.length(), textBounds);
    }

    public void setText(String text) {
        this.text = text;
        paint.getTextBounds(text, 0, text.length(), textBounds);
    }

    public void draw(Canvas canvas) {
        canvas.drawText(text, x, y + (textBounds.bottom - textBounds.top) / 2, paint);
    }
}
