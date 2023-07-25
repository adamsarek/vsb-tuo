package game;

import tools.Highscore;

import java.io.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Locale;

public class Highscores{
    private final File file;
    private ArrayList<Highscore> ranks = new ArrayList<>();
    private Highscore rank = new Highscore(0, 0);

    public Highscores(String fileName){
        this.file = new File(fileName);

        // Create file if not already
        try{
            this.file.createNewFile();
        } catch(IOException e){
            e.printStackTrace();
        }

        // Load
        load();
    }

    public void load(){
        // Remove previous ranks
        ranks.clear();

        // Read from file
        FileReader fr = null;
        BufferedReader br = null;
        try{
            fr = new FileReader(file);
            br = new BufferedReader(fr);

            // Get highscore ranks
            String highscore;
            Highscore rank = new Highscore(0, 0);
            while((highscore = br.readLine()) != null){
                String[] highscoreLine = highscore.split("/");
                rank = new Highscore(Double.parseDouble(highscoreLine[0]), Double.parseDouble(highscoreLine[1]));
                ranks.add(rank);
            }
            this.rank = rank;
            Collections.sort(ranks, Comparator.comparing(Highscore::getScore).reversed().thenComparing(Highscore::getTime));
        } catch(IOException e){
            e.printStackTrace();
        } finally{
            try{
                br.close();
                fr.close();
            } catch(IOException e){
                e.printStackTrace();
            }
        }
    }

    public void save(double score, double time){
        String newHighscore = score + "/" + time;

        // Write to file
        FileWriter fw = null;
        BufferedWriter bw = null;
        try{
            fw = new FileWriter(file, true);
            bw = new BufferedWriter(fw);
            bw.write(newHighscore);
            bw.newLine();
            bw.flush();

            // Set new highscore ranks
            this.rank = new Highscore(score, time);
            ranks.add(rank);
            Collections.sort(ranks, Comparator.comparing(Highscore::getScore).reversed().thenComparing(Highscore::getTime));
        } catch(IOException e){
            e.printStackTrace();
        } finally{
            try{
                bw.close();
                fw.close();
            } catch(IOException e){
                e.printStackTrace();
            }
        }
    }

    public String toString(){
        String string = new String();

        string += String.format("   \t%1$-4s\t%2$-5s\t%3$-11s\t   \n", "Rank", "Score", "Time");
        boolean wasMyRank = false;
        for(int i = 0; i < ranks.size(); i++){
            // True = current rank is mine
            boolean myRank = (ranks.get(i).getScore() == rank.getScore() && ranks.get(i).getTime() == rank.getTime());

            // Display 10+1 ranks if myRank is not in top 10
            // Otherwise display top 10
            if(i < 10 || (myRank && !wasMyRank)){
                if(i >= 10){string += "\n";}

                double time = ranks.get(i).getTime();
                double timeH = Math.floor(time / 3600);
                double timeM = Math.floor((time - timeH * 60) / 60);
                double timeS = Math.floor(time - timeH * 3600 - timeM * 60);
                double timeMS = Math.floor((time - timeH * 3600 - timeM * 60 - timeS) * 1000);
                String timeText = ((int) timeH > 0 ? (int) timeH + ":" : "")
                        + ((int) timeH > 0 && (int) timeM < 10 ? "0" + (int) timeM + ":" : (int) timeM + ":")
                        + ((int) timeS >= 10 ? (int) timeS : "0" + (int) timeS)
                        + "." + ((int) timeMS >= 100 ? (int) timeMS : ((int) timeMS >= 10 ? "0" + (int) timeMS : "00" + (int) timeMS));

                String rank = (i > 0 && ranks.get(i - 1).getScore() == ranks.get(i).getScore() && ranks.get(i - 1).getTime() == ranks.get(i).getTime() ? "" : i + 1 + "");

                string += String.format(Locale.ROOT, "%1$-3s\t%2$-4s\t%3$-5s\t%4$-11s\t%5$-3s\n", (myRank && !wasMyRank ? "-->" : ""), rank, (int) ranks.get(i).getScore(), timeText, (myRank && !wasMyRank ? "<--" : ""));
            }

            // True = my rank was already displayed
            if(myRank){wasMyRank = true;}
        }

        return string;
    }
}
