package game.objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.Direction;
import tools.Position;
import tools.Size;
import tools.virtual.BounceableObject;

public class Ball extends BounceableObject {
    private boolean released = false;

    public Ball(Position position, Size size, Color color, Direction direction, GraphicsContext graphicsContext){
        super(position, size, color, direction, graphicsContext);
    }

    // Release
    public void release(){
        this.released = true;
    }
    public boolean isReleased(){
        return this.released;
    }

    // Move
    @Override
    public void move() {
        this.position = new Position(this.position.x + this.direction.dX, this.position.y + this.direction.dY);
    }

    // Paint
    @Override
    public void paint() {
        this.graphicsContext.setFill(this.color);
        this.graphicsContext.fillOval(this.position.x - this.size.width * 0.5, this.position.y - this.size.height * 0.5, this.size.width, this.size.height);
    }
}