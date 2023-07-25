package game.objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;
import tools.Position;
import tools.interfaces.IPaintable;

public class Info implements IPaintable {
    private final Position position;
    private final Color color;
    private final GraphicsContext graphicsContext;
    private final String title;
    private final String text;

    public Info(Position position, Color color, GraphicsContext graphicsContext, String title, String text){
        this.position = position;
        this.color = color;
        this.graphicsContext = graphicsContext;
        this.title = title;
        this.text = text;
    }

    @Override
    public void paint(){
        graphicsContext.setTextAlign(TextAlignment.CENTER);
        graphicsContext.setFill(this.color);
        graphicsContext.setFont(new Font("Consolas", 24));
        graphicsContext.fillText(this.title, this.position.x, this.position.y);
        graphicsContext.setFont(new Font("Consolas", 16));
        graphicsContext.fillText(this.text, this.position.x, this.position.y+24);
    }
}
