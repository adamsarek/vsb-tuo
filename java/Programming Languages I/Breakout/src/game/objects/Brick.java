package game.objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.Position;
import tools.Size;
import tools.Speed;
import tools.virtual.Object;

public class Brick extends Object {
    public final Speed speedEffect;
    public final double scoreEffect;

    public Brick(Position position, Size size, Color color, GraphicsContext graphicsContext, Speed speedEffect, double scoreEffect){
        super(position, size, color, graphicsContext);
        this.speedEffect = speedEffect;
        this.scoreEffect = scoreEffect;
    }

    // Paint
    @Override
    public void paint() {
        this.graphicsContext.setFill(this.color);
        this.graphicsContext.fillRect(this.position.x, this.position.y, this.size.width, this.size.height);
    }
}
