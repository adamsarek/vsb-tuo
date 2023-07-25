package com.example.snake;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class MainActivity extends AppCompatActivity {
    private SharedPreferences sp;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final EditText playerNameText = findViewById(R.id.playerNameText);
        final Button playBtn = findViewById(R.id.playBtn);
        final Button highscoresBtn = findViewById(R.id.highscoresBtn);

        // Load
        sp = getSharedPreferences("Game", Context.MODE_PRIVATE);

        final String playerName = sp.getString("playerName", null);
        if(playerName != null && playerName.length() > 0) {
            playerNameText.setText(playerName);
            playBtn.setEnabled(true);
        }

        // Events
        playerNameText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                final String playerName = s.toString();

                // Save
                final SharedPreferences.Editor spEditor = sp.edit();
                spEditor.putString("playerName", playerName);
                spEditor.apply();

                if(playerName.length() > 0) {
                    playBtn.setEnabled(true);
                }
                else {
                    playBtn.setEnabled(false);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {

            }
        });

        playBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent gameIntent = new Intent(MainActivity.this, GameActivity.class);
                startActivity(gameIntent);
            }
        });

        highscoresBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent highscoresIntent = new Intent(MainActivity.this, HighscoresActivity.class);
                startActivity(highscoresIntent);
            }
        });
    }
}