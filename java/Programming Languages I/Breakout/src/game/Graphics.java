package game;

import game.objects.Ball;
import game.objects.Brick;
import game.objects.Info;
import game.objects.Paddle;
import javafx.animation.Animation;
import javafx.animation.KeyFrame;
import javafx.animation.Timeline;
import javafx.geometry.Insets;
import javafx.scene.Parent;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.FontWeight;
import javafx.scene.text.Text;
import javafx.scene.text.TextFlow;
import javafx.util.Duration;
import tools.*;
import tools.enumerations.Direction8;
import tools.enumerations.PlayStatus;
import tools.interfaces.IPaintable;
import tools.virtual.BounceableObject;
import tools.virtual.MovableObject;
import tools.virtual.Object;

import java.util.ArrayList;

public class Graphics{
    // Application
    private final Window window;

    // GUI
    private AnchorPane root = new AnchorPane();
    private final AnchorPane header = new AnchorPane();
    private final Text timeCounterKey = new Text();
    private final Text timeCounterValue = new Text();
    private final Text scoreCounterKey = new Text();
    private final Text scoreCounterValue = new Text();
    private final TextFlow timeCounter = new TextFlow(timeCounterKey, timeCounterValue);
    private final TextFlow scoreCounter = new TextFlow(scoreCounterKey, scoreCounterValue);
    private final StackPane body = new StackPane();
    private final Canvas canvas = new Canvas();
    private final GraphicsContext graphicsContext = canvas.getGraphicsContext2D();

    // Game
    private Player player = new Player();
    private Highscores highscores = new Highscores("highscores.txt");

    // Game objects
    private ArrayList<Object> bricks;
    private BounceableObject ball;
    private MovableObject paddle;
    private IPaintable info = new Info(new Position(0, 0), Color.TRANSPARENT, graphicsContext, "", "");

    // Time
    private PlayStatus playStatus;
    private double time;
    private final Timeline timeline = new Timeline(250, new KeyFrame(Duration.millis(1000 / 250), event -> {
        time += this.timeline.getCurrentTime().toSeconds() / this.timeline.getCurrentRate();
        double timeH = Math.floor(time / 3600);
        double timeM = Math.floor((time - timeH * 60) / 60);
        double timeS = Math.floor(time - timeH * 3600 - timeM * 60);
        String timeText = ((int)timeH > 0 ? (int)timeH+":" : "")
                + ((int)timeH > 0 && (int)timeM < 10 ? "0"+(int)timeM+":" : (int)timeM+":")
                + ((int)timeS >= 10 ? (int)timeS : "0"+(int)timeS);
        timeCounterValue.setText(timeText);

        moveBall();
    }));
    private final Timeline timelinePaddle = new Timeline(250, new KeyFrame(Duration.millis(1000 / 250), event -> {
        movePaddle();
        paintCanvas();
    }));

    public Graphics(Window window){
        this.window = window;
        this.timeline.setCycleCount(Timeline.INDEFINITE);
        this.timelinePaddle.setCycleCount(Timeline.INDEFINITE);
    }

    public Parent createContent(){
        // Root
        root.setId("root");
        root.setPrefSize(window.size.width, window.size.height);
        root.setBackground(new Background(new BackgroundFill(Color.BLACK,null,null)));

        // Header
        header.setId("header");
        root.getChildren().add(header);
        root.setLeftAnchor(header,0.0);
        root.setRightAnchor(header,0.0);
        root.setTopAnchor(header,0.0);
        header.setBackground(new Background(new BackgroundFill(Color.WHITE,null,null)));
        header.setPadding(new Insets(8));

        // Time counter
        timeCounter.setId("timeCounter");
        header.getChildren().add(timeCounter);
        header.setLeftAnchor(timeCounter,0.0);
        header.setTopAnchor(timeCounter,0.0);

        // Time counter key
        timeCounterKey.setId("timeCounterKey");
        timeCounterKey.setFill(Color.BLACK);
        timeCounterKey.setFont(Font.font("Helvetica", FontWeight.BOLD, 16));
        timeCounterKey.setText("Time: ");

        // Time counter value
        timeCounterValue.setId("timeCounterValue");
        timeCounterValue.setFill(Color.BLACK);
        timeCounterValue.setFont(Font.font("Helvetica",16));
        timeCounterValue.setText("0:00");

        // Score counter
        scoreCounter.setId("scoreCounter");
        header.getChildren().add(scoreCounter);
        header.setRightAnchor(scoreCounter,0.0);
        header.setTopAnchor(scoreCounter,0.0);

        // Score counter key
        scoreCounterKey.setId("scoreCounterKey");
        scoreCounterKey.setFill(Color.BLACK);
        scoreCounterKey.setFont(Font.font("Helvetica", FontWeight.BOLD, 16));
        scoreCounterKey.setText("Score: ");

        // Score counter value
        scoreCounterValue.setId("scoreCounterValue");
        scoreCounterValue.setFill(Color.BLACK);
        scoreCounterValue.setFont(Font.font("Helvetica",16));
        scoreCounterValue.setText("0");

        // Body
        body.setId("body");
        root.getChildren().add(body);
        root.setLeftAnchor(body,0.0);
        root.setRightAnchor(body,0.0);
        root.setTopAnchor(body,header.getBoundsInParent().getHeight() + header.getPadding().getTop() + header.getPadding().getBottom());
        root.setBottomAnchor(body,0.0);

        // Canvas
        body.setId("canvas");
        body.getChildren().add(canvas);
        canvas.setWidth(window.size.width);
        canvas.setHeight(window.size.height - header.getBoundsInParent().getHeight() + header.getPadding().getTop() + header.getPadding().getBottom());

        return this.root;
    }

    public void start(){
        // Spawn objects
        spawnBricks();
        spawnBall();
        spawnPaddle();

        // Show controls
        String text = new String();
        text += String.format("%1$-5s\t%2$-17s\n", "Key", "Event");
        text += String.format("%1$-5s\t%2$-17s\n", "SPACE", "Release ball");
        text += String.format("%1$-5s\t%2$-17s\n", "LEFT", "Move paddle left");
        text += String.format("%1$-5s\t%2$-17s\n", "RIGHT", "Move paddle right");
        text += String.format("%1$-5s\t%2$-17s\n", "P", "Pause game");
        text += String.format("%1$-5s\t%2$-17s\n", "R", "Restart game");
        info = new Info(new Position(canvas.getBoundsInParent().getWidth() * 0.5,canvas.getBoundsInParent().getHeight() * 0.5), Color.WHITE, graphicsContext, "CONTROLS", text);

        playStatus = PlayStatus.STARTED;
        timelinePaddle.play();
    }

    public void play(){
        info = new Info(new Position(0, 0), Color.TRANSPARENT, graphicsContext, "", "");

        playStatus = PlayStatus.PLAYING;
        timeline.play();
    }

    public void pause(){
        info = new Info(new Position(canvas.getBoundsInParent().getWidth() * 0.5,canvas.getBoundsInParent().getHeight() * 0.5), Color.WHITE, graphicsContext, "GAME PAUSED", "Press any key to resume the game.");

        playStatus = PlayStatus.PAUSED;
        timeline.pause();
    }

    public void end(){
        playStatus = PlayStatus.ENDED;
        timeline.stop();
        timelinePaddle.stop();

        // Save result
        highscores.save(player.getScore(), time);

        // Show results
        info = new Info(new Position(canvas.getBoundsInParent().getWidth() * 0.5,canvas.getBoundsInParent().getHeight() * 0.5), Color.WHITE, graphicsContext, "HIGHSCORE", highscores.toString() + "\nPress R to restart the game.");
        paintCanvas();
    }

    public void restart(){
        // Clear after previous game
        timeline.stop();
        timeline.setRate(1);
        time = 0;
        player.setScore(0);
        timeCounterValue.setText("0:00");
        scoreCounterValue.setText("0");
        despawnBricks();
        despawnBall();
        despawnPaddle();

        // Start new game
        start();
    }

    public void keyPress(KeyEvent e){
        KeyCode code = e.getCode();

        // Reset game
        if(code == KeyCode.R){
            restart();
        }
        if(playStatus != PlayStatus.ENDED){
            // Play/Pause game
            if(code == KeyCode.P && timeline.getStatus() == Animation.Status.RUNNING){
                pause();
            }
            else if(code == KeyCode.SPACE || ((Ball)ball).isReleased()){
                ((Ball)ball).release();
                play();
            }
            // Paddle movement
            if(code == KeyCode.LEFT){
                paddle.setDirection(Direction8.WEST.direction);
            }
            else if(code == KeyCode.RIGHT){
                paddle.setDirection(Direction8.EAST.direction);
            }
        }
    }

    public void keyRelease(KeyEvent e){
        KeyCode code = e.getCode();

        // Paddle movement
        if(code == KeyCode.LEFT || code == KeyCode.RIGHT){
            paddle.setDirection(Direction8.NONE.direction);
        }
    }

    private void paintCanvas(){
        clearCanvas();

        // Bricks
        for(Object brick : bricks){
            ((Brick)brick).paint();
        }

        // Ball
        ball.paint();

        // Paddle
        paddle.paint();

        // Info
        info.paint();
    }

    private void clearCanvas(){
        graphicsContext.clearRect(0, 0, canvas.getWidth(), canvas.getHeight());
    }

    private void spawnBricks(){
        // Grid
        int maxRowCount = 5;
        int maxColCount = 20;
        int rowCount = 5;
        int colCount = 20;

        // Sizes
        Size spawnAreaSize = new Size(canvas.getWidth() * 0.995, canvas.getHeight() * 0.2);
        double brickPadding = canvas.getWidth() * 0.0025;
        Size brickSize = new Size(spawnAreaSize.width / maxColCount - 2 * brickPadding,spawnAreaSize.height / maxRowCount - 2 * brickPadding);

        // Positions
        Position spawnAreaPos = new Position(brickPadding + (canvas.getWidth() - (colCount * (brickSize.width + brickPadding * 2))) * 0.5, canvas.getHeight() * 0.1);
        Position brickPos = new Position(spawnAreaPos.x, spawnAreaPos.y);

        bricks = new ArrayList<>();
        for(int i = 0; i < rowCount; i++){
            Speed brickSpeedEffect = new Speed(rowCount-i-1);
            for(int j = 0; j < colCount; j++){
                Brick brick = new Brick(brickPos, brickSize, Color.WHITE, graphicsContext, brickSpeedEffect, brickSpeedEffect.speedExp+1);
                bricks.add(brick);

                brickPos = new Position(brickPos.x + brickSize.width + 2 * brickPadding, brickPos.y);
            }
            brickPos = new Position(spawnAreaPos.x, brickPos.y + brickSize.height + 2 * brickPadding);
        }
    }

    private void spawnBall(){
        Position ballPos = new Position(canvas.getWidth() * 0.5, canvas.getHeight() * 0.96);
        Size ballSize = new Size(canvas.getHeight() * 0.02, canvas.getHeight() * 0.02);
        Color ballColor = Color.WHITE;
        Direction ballDirection = new Direction(15 + 150 * Math.random());

        ball = new Ball(ballPos, ballSize, ballColor, ballDirection, graphicsContext);
    }

    private void spawnPaddle(){
        Position paddlePos = new Position(canvas.getWidth() * 0.45, canvas.getHeight() * 0.98);
        Size paddleSize = new Size(canvas.getWidth() * 0.1, canvas.getHeight() * 0.01);
        Color paddleColor = Color.WHITE;
        Direction paddleDirection = new Direction(Direction8.NONE.direction.angle);

        paddle = new Paddle(paddlePos, paddleSize, paddleColor, paddleDirection, graphicsContext);
    }

    private void despawnBricks(){
        for(int i = 0; i < bricks.size();){
            despawnBrick(bricks.get(i));
        }
        bricks = null;
    }

    private void despawnBrick(Object brick){
        bricks.remove(brick);
    }

    private void despawnBall(){
        ball = null;
    }

    private void despawnPaddle(){
        paddle = null;
    }

    private void moveBall(){
        ball.move();

        // Brick bounces
        int bounceType = -1;
        for(int i = 0; i < bricks.size(); i++){
            Object brick = bricks.get(i);
            boolean brickBroken = false;

            // Left & Right bounce
            if(ball.getPosition().y >= brick.getPosition().y && ball.getPosition().y <= brick.getPosition().y + brick.getSize().height){
                if(ball.getPosition().x - ball.getSize().width * 0.5 <= brick.getPosition().x + brick.getSize().width && ball.getPosition().x - ball.getSize().width * 0.5 >= brick.getPosition().x
                        || ball.getPosition().x + ball.getSize().width * 0.5 >= brick.getPosition().x && ball.getPosition().x + ball.getSize().width * 0.5 <= brick.getPosition().x + brick.getSize().width){
                    brickBroken = true;
                    bounceType = 0;
                }
            }

            // Top & Bottom bounce
            if(ball.getPosition().x >= brick.getPosition().x && ball.getPosition().x <= brick.getPosition().x + brick.getSize().width){
                if(ball.getPosition().y - ball.getSize().height * 0.5 <= brick.getPosition().y + brick.getSize().height && ball.getPosition().y - ball.getSize().height * 0.5 >= brick.getPosition().y
                        || ball.getPosition().y + ball.getSize().height * 0.5 >= brick.getPosition().y && ball.getPosition().y + ball.getSize().height * 0.5 <= brick.getPosition().y + brick.getSize().height){
                    brickBroken = true;
                    bounceType = 1;
                }
            }

            if(brickBroken){
                // Change ball speed
                if(((Brick)brick).speedEffect.speed > timeline.getCurrentRate()){
                    timeline.setRate(((Brick)brick).speedEffect.speed);
                }

                // Add score
                player.addScore(((Brick)brick).scoreEffect);
                scoreCounterValue.setText(Integer.toString((int)player.getScore()));

                // Remove brick
                despawnBrick(brick);

                // End game
                if(bricks.size() == 0){
                    end();
                }
            }
        }
        if(bounceType == 0){
            ball.bounceHorizontally();
        }
        else if(bounceType == 1){
            ball.bounceVertically();
        }

        // Paddle bounces
        double paddleMinAngle = 15;
        double paddleMaxAngle = 165;
        if(ball.getPosition().x >= paddle.getPosition().x && ball.getPosition().x <= paddle.getPosition().x + paddle.getSize().width
                && ball.getPosition().y + ball.getSize().height * 0.5 >= paddle.getPosition().y && ball.getPosition().y + ball.getSize().height * 0.5 <= paddle.getPosition().y + paddle.getSize().height){
            ball.setDirection(new Direction(paddleMaxAngle - (ball.getPosition().x - paddle.getPosition().x) / paddle.getSize().width * (paddleMaxAngle - paddleMinAngle)));
        }

        // Window bounces
        if(ball.getPosition().x - ball.getSize().width * 0.5 <= 0){
            ball.bounceHorizontally();
        }
        if(ball.getPosition().x + ball.getSize().width * 0.5 >= canvas.getWidth()){
            ball.bounceHorizontally();
        }
        if(ball.getPosition().y - ball.getSize().height * 0.5 <= 0) {
            ball.bounceVertically();
        }
        if(ball.getPosition().y + ball.getSize().height * 0.5 >= canvas.getHeight()){
            end();
        }
    }

    private void movePaddle(){
        Position paddleOldPos = paddle.getPosition();
        paddle.move();

        // Move unreleased ball
        if(!((Ball)ball).isReleased()){
            double paddleMoveStep = paddle.getPosition().x - paddleOldPos.x;
            ball.setPosition(new Position(ball.getPosition().x + paddleMoveStep, ball.getPosition().y ));
        }
    }
}
