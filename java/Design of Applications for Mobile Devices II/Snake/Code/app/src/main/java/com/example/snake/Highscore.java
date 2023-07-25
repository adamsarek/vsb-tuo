package com.example.snake;

import java.text.DecimalFormat;

public class Highscore {
    private int rank = 0;
    public final long saveTime;
    public final String playerName;
    public final int score;
    public final long totalTime;
    public final double lat;
    public final double lon;

    public Highscore(long saveTime, String playerName, int score, long totalTime, double lat, double lon) {
        this.saveTime = saveTime;
        this.playerName = playerName;
        this.score = score;
        this.totalTime = totalTime;
        this.lat = lat;
        this.lon = lon;
    }

    public void setRank(int rank) {
        this.rank = rank;
    }

    public boolean isLastSaved(long lastSaveTime) {
        if(lastSaveTime == saveTime) {
            return true;
        }
        return false;
    }

    private String getDisplayTime() {
        long t = totalTime;

        long h = t / (60 * 60 * 1000);   t -= h * 60 * 60 * 1000;
        long m = t / (60 * 1000);        t -= m * 60 * 1000;
        long s = t / 1000;               t -= s * 1000;
        long ms = t;

        String H = (h > 0 ? h + ":" : "");
        String M = (h > 0 && m < 10 ? "0" + m : m) + ":";
        String S = (s < 10 ? "0" : "") + s + ".";
        String MS = (ms < 10 ? "00" : (ms < 100 ? "0" : "")) + ms;

        return H + M + S + MS;
    }

    @Override
    public String toString() {
        String rankEnd = "th";
        if(rank % 10 == 1 && rank != 11) {
            rankEnd = "st";
        }
        else if(rank % 10 == 2 && rank != 12) {
            rankEnd = "nd";
        }
        else if(rank % 10 == 3 && rank != 13) {
            rankEnd = "rd";
        }

        DecimalFormat df2 = new DecimalFormat("#.##");

        return rank + rankEnd + " | Player: " + playerName + " | Score: " + score + " | Time: " + getDisplayTime() + "\nGPS: " + df2.format(lat) + ", " + df2.format(lon);
    }
}
