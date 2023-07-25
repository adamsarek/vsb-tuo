package tools.virtual;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import tools.Position;
import tools.Size;
import tools.interfaces.*;

public abstract class Object implements IPositionable, IResizable, IColorable, IPaintable {
    protected Position position;
    protected Size size;
    protected Color color;
    protected GraphicsContext graphicsContext;

    public Object(Position position, Size size, Color color, GraphicsContext graphicsContext){
        this.position = position;
        this.size = size;
        this.color = color;
        this.graphicsContext = graphicsContext;
    }

    // Position
    @Override
    public Position getPosition() {
        return this.position;
    }
    @Override
    public void setPosition(Position position) {
        this.position = position;
    }

    // Size
    @Override
    public Size getSize() {
        return this.size;
    }
    @Override
    public void setSize(Size size) {
        this.size = size;
    }

    // Color
    @Override
    public Color getColor() {
        return this.color;
    }
    @Override
    public void setColor(Color color) {
        this.color = color;
    }
}
