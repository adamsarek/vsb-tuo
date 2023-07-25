package com.example.snake;

import java.util.Random;

public class Apple {
    private final int gameWidth;
    private final int gameHeight;

    // Snake
    private Block block;

    public Apple(int gameWidth, int gameHeight, Block[] denyBlocks) {
        this.gameWidth = gameWidth;
        this.gameHeight = gameHeight;

        Random random = new Random();
        int x = random.nextInt(gameWidth);
        int y = random.nextInt(gameHeight);
        boolean placed = false;
        while(!placed) {
            placed = true;
            for(int i = 0; i < denyBlocks.length; i++) {
                if(denyBlocks[i].getX() == x && denyBlocks[i].getY() == y) {
                    x = random.nextInt(gameWidth);
                    y = random.nextInt(gameHeight);
                    placed = false;
                    break;
                }
            }
        }
        block = new Block(x, y);
    }

    public Block getBlock() {
        return block;
    }
}
