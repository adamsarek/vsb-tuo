package tools;

public class Direction{
    public final double angle;
    public final double dX;
    public final double dY;

    public Direction(double angle){
        this.angle = angle;
        this.dX = Math.cos(this.angle / 180 * Math.PI);
        this.dY = -Math.sin(this.angle / 180 * Math.PI);
    }
    public double getInvertedXAngle(){
        double newAngle = 180 - this.angle;
        return (newAngle < 0 ? 360 + newAngle : newAngle);
    }
    public double getInvertedYAngle(){
        return (360 - this.angle);
    }
}
