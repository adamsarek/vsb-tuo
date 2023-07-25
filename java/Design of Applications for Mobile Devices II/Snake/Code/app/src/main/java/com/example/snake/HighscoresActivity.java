package com.example.snake;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.Set;

public class HighscoresActivity extends AppCompatActivity {
    private SharedPreferences sp;

    private String playerName;
    private ArrayList<String> highscores = new ArrayList<String>();
    private ArrayList<Highscore> highscoreList = new ArrayList<Highscore>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_highscores);

        // Load
        sp = getSharedPreferences("Game", Context.MODE_PRIVATE);
        playerName = sp.getString("playerName", null);
        Set highscoresSP = sp.getStringSet("highscores", null);

        if(highscoresSP != null){
            highscores.addAll(highscoresSP);

            long lastSaveTime = 0;
            for(String highscore : highscores) {
                String[] highscoreParts = highscore.split(";");

                long saveTime = Long.parseLong(highscoreParts[0]);
                String playerName = highscoreParts[1];
                int score = Integer.parseInt(highscoreParts[2]);
                long totalTime = Long.parseLong(highscoreParts[3]);
                double lat = Double.parseDouble(highscoreParts[4]);
                double lon = Double.parseDouble(highscoreParts[5]);

                highscoreList.add(new Highscore(saveTime, playerName, score, totalTime, lat, lon));

                if(saveTime > lastSaveTime) { lastSaveTime = saveTime; }
            }

            // Sort highscores
            highscoreList.sort(new Comparator<Highscore>() {
                @Override
                public int compare(Highscore a, Highscore b) {
                    if(a.score < b.score || a.score == b.score && a.totalTime > b.totalTime) {
                        return 1;
                    }
                    else if(a.score == b.score && a.totalTime == b.totalTime) {
                        return 0;
                    }
                    return -1;
                }
            });

            // Set ranks
            int lastSaveId = 0;
            for(int i = 0; i < highscoreList.size(); i++) {
                highscoreList.get(i).setRank(i + 1);

                if(highscoreList.get(i).isLastSaved(lastSaveTime)) {
                    lastSaveId = i;
                }
            }

            // Display top 10 highscores
            ArrayList<String> highscoresStringList = new ArrayList<String>();
            if(lastSaveId < 10) {
                for(int i = 0; i < (highscoreList.size() > 10 ? 10 : highscoreList.size()); i++) {
                    highscoresStringList.add(String.valueOf(highscoreList.get(i)));
                    //System.out.println(highscoreList.get(i) + (lastSaveId == i ? " <--" : ""));
                }
            }
            else {
                for(int i = 0; i < 10; i++) {
                    highscoresStringList.add(String.valueOf(highscoreList.get(i)));
                    //System.out.println(highscoreList.get(i));
                }
                highscoresStringList.add(String.valueOf(highscoreList.get(lastSaveId)));
                //System.out.println(highscoreList.get(lastSaveId) + " <--");
            }

            ArrayAdapter<Highscore> highscoresArrayAdapter = new ArrayAdapter<Highscore>(this, android.R.layout.simple_list_item_1, highscoreList);
            ListView highscoresListView = findViewById(R.id.highscoresListView);
            highscoresListView.setAdapter(highscoresArrayAdapter);
        }
    }
}