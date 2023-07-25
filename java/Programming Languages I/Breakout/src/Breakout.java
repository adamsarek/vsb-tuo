import game.Graphics;
import javafx.application.Application;
import javafx.scene.Node;
import javafx.scene.Scene;
import javafx.stage.Stage;
import tools.Size;
import tools.Window;

public class Breakout extends Application{
    // Application
    private final Window window = new Window("Breakout", new Size(1280, 720));
    private final Graphics graphics = new Graphics(window);

    @Override
    public void start(Stage stage){
        // Set scene
        stage.setScene(new Scene(graphics.createContent()));
        stage.setTitle(window.title);
        stage.show();

        // KeyPressed Events
        Node root = stage.getScene().getRoot();
        root.setFocusTraversable(true);
        root.requestFocus();
        root.setOnKeyPressed(e -> graphics.keyPress(e));
        root.setOnKeyReleased(e -> graphics.keyRelease(e));

        // Start game
        graphics.start();
    }

    public static void main(String[] args){
        launch(args);
    }
}
