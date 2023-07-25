package com.example.snake;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.AudioManager;
import android.media.SoundPool;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.DisplayMetrics;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;

public class GameActivity extends AppCompatActivity implements LocationListener {
    private SharedPreferences sp;
    private GameView gameView;
    private Canvas canvas;

    // Frame
    private long lastFrameTime = 0;
    private final int fps = 5;

    // Screen
    private int screenWidth;
    private int screenHeight;

    // Game sizes
    private final int blockMargin = 10;
    private final int gameTopBarHeight = 80;
    private final int gameBottomBarHeight = 240;
    private final int gameWidth = 20;
    private int gameHeight;
    private int gameTopMargin;
    private int gameLeftMargin;
    private int blockSize;

    // Game
    private String playerName;
    private ArrayList<String> highscores = new ArrayList<String>();
    private Apple apple;
    private Snake snake;
    private CanvasText scoreText;
    private CanvasText playerText;
    private CanvasText timeText;
    private CanvasButton muteButton;
    private int score = 0;
    private long resumeTime = 0;
    private long totalTime = 0;

    private Thread playThread = null;
    private volatile boolean playing = false;
    private volatile boolean muted = false;
    private volatile boolean touched = false;
    private float sTX;
    private float sTY;

    // Sounds
    private SoundPool soundPool = new SoundPool(2, AudioManager.STREAM_MUSIC, 0);
    private int dieSound;
    private int dirSound;
    private int eatSound;

    // GPS
    private LocationManager locationManager;
    private double lat = 0;
    private double lon = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Load
        sp = getSharedPreferences("Game", Context.MODE_PRIVATE);
        playerName = sp.getString("playerName", null);
        Set<String> highscoresSet = sp.getStringSet("highscores", null);
        if(highscoresSet != null) {
            highscores.addAll(highscoresSet);
        }

        // Sounds
        dieSound = soundPool.load(this, R.raw.die, 1);
        dirSound = soundPool.load(this, R.raw.dir, 1);
        eatSound = soundPool.load(this, R.raw.eat, 1);

        gameView = new GameView(this);
        setContentView(gameView);
    }

    @Override
    protected void onResume() {
        super.onResume();

        resume();
    }

    @Override
    protected void onPause() {
        super.onPause();

        pause();
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);

        updateLocation();
        saveHighscore();
    }

    @Override
    public void onLocationChanged(Location location) {
        lat = location.getLatitude();
        lon = location.getLongitude();
    }

    @Override
    public void onProviderEnabled(@NonNull String provider) {
        System.out.println("Enabled");
    }

    @Override
    public void onProviderDisabled(@NonNull String provider) {
        System.out.println("Disabled");
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {
        System.out.println("Status changed");
    }

    private void resume() {
        playing = true;
        resumeTime = System.currentTimeMillis();
        playThread = new Thread(gameView);
        playThread.start();
    }

    private void pause() {
        playThread.interrupt();
        playing = false;
        totalTime += (System.currentTimeMillis() - resumeTime);
    }

    private void end() {
        pause();

        // Get GPS location
        if(ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);
        }
        else {
            updateLocation();
            saveHighscore();
        }
    }

    private void updateLocation() {
        if(ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED || ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
            String bestProvider = locationManager.getBestProvider(new Criteria(), true);
            if(bestProvider != null) {
                locationManager.requestSingleUpdate(bestProvider, this, null);
                Location location = locationManager.getLastKnownLocation(bestProvider);
                if(location != null) {
                    lat = location.getLatitude();
                    lon = location.getLongitude();
                }
            }
        }
    }

    private void saveHighscore() {
        if(score > 0) {
            String newHighscore = System.currentTimeMillis() + ";" + playerName + ";" + score + ";" + totalTime + ";" + lat + ";" + lon;

            Set<String> highscoresSet = new HashSet<String>();
            highscoresSet.addAll(highscores);
            highscoresSet.add(newHighscore);

            final SharedPreferences.Editor spEditor = sp.edit();
            spEditor.putStringSet("highscores", highscoresSet);
            spEditor.apply();

            openHighscores();
        }
        else {
            finish();
        }
    }

    private void openHighscores() {
        Intent highscoresIntent = new Intent(GameActivity.this, HighscoresActivity.class);
        startActivity(highscoresIntent);
        finish();
    }

    private void mute() {
        muted = true;
        muteButton.setIcon(getResources().getDrawable(R.drawable.unmute, null));
    }

    private void unmute() {
        muted = false;
        muteButton.setIcon(getResources().getDrawable(R.drawable.mute, null));
    }

    private void toggleMuteState() {
        if(muted) {
            unmute();
        }
        else {
            mute();
        }
    }

    private String getDisplayTime() {
        long time = totalTime;
        if(playing) {
            time += (System.currentTimeMillis() - resumeTime);
        }

        long h = time / (60 * 60 * 1000);   time -= h * 60 * 60 * 1000;
        long m = time / (60 * 1000);        time -= m * 60 * 1000;
        long s = time / 1000;

        String H = (h > 0 ? h + ":" : "");
        String M = (h > 0 && m < 10 ? "0" + m : m) + ":";
        String S = (s < 10 ? "0" + s : String.valueOf(s));

        return H + M + S;
    }

    private void update() {
        if(!snake.move()) {
            if(!muted) { soundPool.play(dieSound, 1.0f, 1.0f, 1, 0, 1.0f); }
            end();
        }

        Block appleBlock = apple.getBlock();
        Block[] snakeBlocks = snake.getBlocks();
        if(snakeBlocks[0].getX() == appleBlock.getX() && snakeBlocks[0].getY() == appleBlock.getY()) {
            if(!muted) { soundPool.play(eatSound, 1.0f, 1.0f, 1, 0, 1.0f); }
            snake.eat();
            score++;
            apple = new Apple(gameWidth, gameHeight, snakeBlocks);
        }

        scoreText.setText(String.valueOf(score));
        timeText.setText(getDisplayTime());
    }

    private void redraw() {
        gameView.postInvalidate(0,0,gameView.getWidth(), gameView.getHeight());
    }

    private class GameView extends SurfaceView implements Runnable{
        private SurfaceHolder holder;

        private GameView(Context context) {
            super(context);
            holder = getHolder();

            DisplayMetrics displayMetrics = new DisplayMetrics();
            ((Activity) getContext()).getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);

            screenWidth = displayMetrics.widthPixels;
            screenHeight = displayMetrics.heightPixels;

            // Calculate blockSize and gameHeight
            blockSize = (screenWidth - blockMargin * (gameWidth + 1)) / gameWidth;
            gameLeftMargin = (screenWidth - (gameWidth * (blockSize + blockMargin) + blockMargin)) / 2;
            gameHeight = (screenHeight - gameTopBarHeight - gameBottomBarHeight - blockMargin) / (blockSize + blockMargin);
            gameTopMargin = gameTopBarHeight + (screenHeight - gameTopBarHeight - gameBottomBarHeight - (gameHeight * (blockSize + blockMargin) + blockMargin)) / 2;

            // Spawn objects
            snake = new Snake(gameWidth, gameHeight);
            apple = new Apple(gameWidth, gameHeight, snake.getBlocks());

            // Create texts
            scoreText = new CanvasText(
                    gameLeftMargin + blockMargin,
                    gameTopMargin / 2,
                    "0",
                    Color.argb(255, 187, 187, 187),
                    Paint.Align.LEFT,
                    40
            );
            playerText = new CanvasText(
                    screenWidth / 2,
                    gameTopMargin / 2,
                    playerName,
                    Color.WHITE,
                    Paint.Align.CENTER,
                    40
            );
            timeText = new CanvasText(
                    screenWidth - gameLeftMargin - blockMargin,
                    gameTopMargin / 2,
                    getDisplayTime(),
                    Color.argb(255, 187, 187, 187),
                    Paint.Align.RIGHT,
                    40
            );

            // Create buttons
            muteButton = new CanvasButton(
                    screenWidth / 2 - gameBottomBarHeight / 4,
                    screenHeight - gameBottomBarHeight + gameBottomBarHeight / 4 - blockMargin,
                    gameBottomBarHeight / 2,
                    gameBottomBarHeight / 2,
                    getResources().getDrawable(R.drawable.mute, null),
                    Color.argb(255, 68, 68, 68),
                    blockMargin,
                    new Callback() {
                        @Override
                        public void invoke() {
                            toggleMuteState();
                        }
                    }
            );

            setWillNotDraw(false);
        }

        @Override
        public void run() {
            Looper.prepare();

            while(playing) {
                long currentFrameTime = System.currentTimeMillis();
                long elapsedFrameTime = currentFrameTime - lastFrameTime;
                long sleepTime = (long)(1000f / fps - elapsedFrameTime);

                canvas = null;
                try {
                    canvas = holder.lockCanvas();

                    if(canvas == null) {
                        playThread.sleep(1);

                        continue;
                    }
                    else if(sleepTime > 0) {
                        playThread.sleep(sleepTime);

                        synchronized(holder) {
                            update();
                        }
                    }
                } catch(Exception e) {
                    e.printStackTrace();
                } finally {
                    if(canvas != null) {
                        holder.unlockCanvasAndPost(canvas);
                        redraw();
                        lastFrameTime = System.currentTimeMillis();
                    }
                }
            }
        }

        @Override
        protected void onDraw(Canvas canvas) {
            super.onDraw(canvas);

            int x, y, w, h;

            Paint paint = new Paint();
            paint.setStyle(Paint.Style.FILL);

            // Background
            paint.setColor(Color.BLACK);
            canvas.drawPaint(paint);

            // Draw texts
            scoreText.draw(canvas);
            playerText.draw(canvas);
            timeText.draw(canvas);

            // Draw buttons
            muteButton.draw(canvas);

            // Grid
            paint.setColor(Color.argb(255, 34, 34, 34));
            for(int i = 0; i < gameWidth; i++) {
                for(int j = 0; j < gameHeight; j++) {
                    x = gameLeftMargin + blockMargin + (blockSize + blockMargin) * i;
                    y = gameTopMargin + blockMargin + (blockSize + blockMargin) * j;
                    canvas.drawRoundRect(x, y, x + blockSize, y + blockSize, blockMargin, blockMargin, paint);
                }
            }

            // Apple
            paint.setColor(Color.argb(255, 34, 221, 34));
            Block appleBlock = apple.getBlock();
            x = gameLeftMargin + blockMargin + (blockSize + blockMargin) * appleBlock.getX();
            y = gameTopMargin + blockMargin + (blockSize + blockMargin) * appleBlock.getY();
            canvas.drawRoundRect(x, y, x + blockSize, y + blockSize, blockMargin, blockMargin, paint);

            // Snake
            Block[] snakeBlocks = snake.getBlocks();
            for(int i = 0; i < snakeBlocks.length; i++) {
                paint.setColor(Color.argb(127 + 128 / snakeBlocks.length * (snakeBlocks.length - i), 255, 255, 255));
                x = gameLeftMargin + blockMargin + (blockSize + blockMargin) * snakeBlocks[i].getX();
                y = gameTopMargin + blockMargin + (blockSize + blockMargin) * snakeBlocks[i].getY();
                canvas.drawRoundRect(x, y, x + blockSize, y + blockSize, blockMargin, blockMargin, paint);
            }
        }

        @Override
        public boolean onTouchEvent(MotionEvent event) {
            final float eTX = event.getX();
            final float eTY = event.getY();

            switch(event.getAction()) {
                case MotionEvent.ACTION_DOWN:
                    if(muteButton.isTouched(eTX, eTY)) { touched = muteButton.touchDown(); }
                    else { touched = true; }

                    if(touched) {
                        sTX = eTX;
                        sTY = eTY;
                    }
                    break;
                case MotionEvent.ACTION_UP:
                    if(muteButton.isTouched(sTX, sTY)) {
                        if(muteButton.isTouched(eTX, eTY)) { touched = muteButton.touchUp(); }
                        else { touched = muteButton.touchDownReset(); }
                    }
                    else {
                        final float dTX = sTX - eTX;
                        final float dTY = sTY - eTY;
                        boolean dirChanged = false;

                        if(Math.abs(dTX) > Math.abs(dTY)) {
                            if(dTX > 0) { dirChanged = snake.changeDirection(3); }
                            else { dirChanged = snake.changeDirection(1); }
                        }
                        else if(Math.abs(dTX) < Math.abs(dTY)) {
                            if(dTY > 0) { dirChanged = snake.changeDirection(0); }
                            else { dirChanged = snake.changeDirection(2); }
                        }

                        if(dirChanged) {
                            if(!muted) { soundPool.play(dirSound, 1.0f, 1.0f, 1, 0, 1.0f); }
                        }
                    }
                    break;
            }

            if(touched) {
                redraw();
                return true;
            }

            return super.onTouchEvent(event);
        }
    }
}