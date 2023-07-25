package tools;

import javafx.scene.paint.Color;

public class Speed{
    public final double speedBase = 1.25;
    public final double speedExp;
    public final double speed;
    public final Color color;

    public Speed(int speedExp){
        this.speedExp = speedExp;
        this.speed = Math.pow(this.speedBase, this.speedExp);
        switch(speedExp){
            case 4:
                this.color = Color.RED;
                break;
            case 3:
                this.color = Color.ORANGE;
                break;
            case 2:
                this.color = Color.YELLOW;
                break;
            case 1:
                this.color = Color.GREEN;
                break;
            default:
                this.color = Color.BLUE;
                break;
        }
    }
}
