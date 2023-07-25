package game;

public class Player{
    private double score = 0;

    public Player(){

    }

    // Score
    public double getScore(){
        return this.score;
    }
    public void setScore(double score){ this.score = score; }
    public void addScore(double score){
        this.score += score;
    }
}
