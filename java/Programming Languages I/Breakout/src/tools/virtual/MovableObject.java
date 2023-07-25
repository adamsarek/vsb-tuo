package tools.virtual;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.*;
import tools.interfaces.IDirectionable;
import tools.interfaces.IMovable;

public abstract class MovableObject extends Object implements IDirectionable, IMovable {
    protected Direction direction;

    public MovableObject(Position position, Size size, Color color, Direction direction, GraphicsContext graphicsContext) {
        super(position, size, color, graphicsContext);
        this.direction = direction;
    }

    // Direction
    @Override
    public Direction getDirection() {
        return this.direction;
    }
    @Override
    public void setDirection(Direction direction) {
        this.direction = direction;
    }
}
