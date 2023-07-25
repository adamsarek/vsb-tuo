package game.objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.Direction;
import tools.Position;
import tools.Size;
import tools.enumerations.Direction8;
import tools.virtual.MovableObject;

public class Paddle extends MovableObject {
    public Paddle(Position position, Size size, Color color, Direction direction, GraphicsContext graphicsContext){
        super(position, size, color, direction, graphicsContext);
    }

    // Move
    @Override
    public void move() {
        Position newPos = null;
        if(this.direction == Direction8.WEST.direction){
            newPos = new Position(this.position.x - 1, this.position.y);
            if(newPos.x <= 0){
                newPos = new Position(0, newPos.y);
            }
        }
        if(this.direction == Direction8.EAST.direction){
            newPos = new Position(this.position.x + 1, this.position.y);
            if(newPos.x + this.size.width >= this.graphicsContext.getCanvas().getWidth()){
                newPos = new Position(this.graphicsContext.getCanvas().getWidth() - this.size.width, newPos.y);
            }
        }
        if(newPos != null){
            this.setPosition(newPos);
        }
    }

    // Paint
    @Override
    public void paint() {
        this.graphicsContext.setFill(this.color);
        this.graphicsContext.fillRect(this.position.x, this.position.y, this.size.width, this.size.height);
    }
}
