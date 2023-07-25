package tools;

public class Highscore{
    private final double score;
    private final double time;

    public Highscore(double score, double time){
        this.score = score;
        this.time = time;
    }

    public double getScore(){
        return this.score;
    }

    public double getTime(){
        return this.time;
    }
}
