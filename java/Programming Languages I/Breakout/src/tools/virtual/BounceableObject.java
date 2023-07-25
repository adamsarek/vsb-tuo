package tools.virtual;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.Direction;
import tools.Position;
import tools.Size;
import tools.interfaces.IBounceable;

public abstract class BounceableObject extends MovableObject implements IBounceable {
    public BounceableObject(Position position, Size size, Color color, Direction direction, GraphicsContext graphicsContext) {
        super(position, size, color, direction, graphicsContext);
    }

    @Override
    public void bounceHorizontally() { this.direction = new Direction(this.direction.getInvertedXAngle()); }
    @Override
    public void bounceVertically() { this.direction = new Direction(this.direction.getInvertedYAngle()); }
}
