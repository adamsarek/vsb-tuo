import pygame
from hero import Hero
from wraith import Wraith
from reaper import Reaper
from scarecrow import Scarecrow
from graveyard.tile_block import Tile_block
from graveyard.grave_object import Grave_object


class Playground:
    def __init__(self):
        """
        Constructor
        """
        self.width = 1920
        self.height = 1080
        self.background = pygame.image.load('background/PNG/2_game_background/2_game_background.png')
        self.playground = self.create_playground()

        # Spawn game objects
        self.game_objects = []
        self.wraith_1 = self.create_game_object(Wraith(self.width, self.height, 881, 300, 'enemy_wraith_1'))
        self.wraith_2 = self.create_game_object(Wraith(self.width, self.height, 441, self.height - 100, 'enemy_wraith_2'))
        self.wraith_3 = self.create_game_object(Wraith(self.width, self.height, 220, 450, 'enemy_wraith_3'))
        self.scarecrow = self.create_game_object(Scarecrow(self.width, self.height, 1440, 724))
        self.reaper = self.create_game_object(Reaper(self.width, self.height, 1440, 350))
        self.hero = self.create_game_object(Hero(self.width, self.height, 881, self.height - 100))

    def create_playground(self):
        """
        Generates the playground with the tiles and graveyard objects
        """
        my_playground = []

        my_playground.append(Tile_block(0, self.height - 100, 0))  # Ground
        my_playground.append(Tile_block(450, 600, 3))  # Island with dead bushes
        my_playground.append(Tile_block(0, 450, 2))  # Island on the left
        my_playground.append(Tile_block(500, 300, 6))  # Island in the middle
        my_playground.append(Tile_block(1280, 350, 4))  # Island on the right (top)
        my_playground.append(Tile_block(800, 700, 3))  # Island on the right (middle)

        # Graveyard objects - trees, tombstones, sign arrows, bushes, bones
        my_playground.append(Grave_object(800, 215, 8))
        my_playground.append(Grave_object(500, 65, 9))
        my_playground.append(Grave_object(900, 65, 9))
        my_playground.append(Grave_object(1200, 120, 9))
        my_playground.append(Grave_object(1620, 120, 9))
        my_playground.append(Grave_object(1450, 280, 11))
        my_playground.append(Grave_object(1550, 280, 11))
        my_playground.append(Grave_object(1650, 280, 11))
        my_playground.append(Grave_object(1170, self.height - 205, 13))
        my_playground.append(Grave_object(1400, 800, 1))
        my_playground.append(Grave_object(1350, 900, 3))
        my_playground.append(Grave_object(200, 890, 8))
        my_playground.append(Grave_object(100, 370, 5))
        my_playground.append(Grave_object(10, 410, 14))
        my_playground.append(Grave_object(500, self.height - 160, 6))
        my_playground.append(Grave_object(700, self.height - 160, 6))
        my_playground.append(Grave_object(550, self.height - 180, 5))
        my_playground.append(Grave_object(1400, self.height - 590, 9))
        my_playground.append(Grave_object(1300, self.height - 420, 7))
        my_playground.append(Grave_object(1450, self.height - 405, 10))
        my_playground.append(Grave_object(1700, self.height - 205, 13))
        my_playground.append(Grave_object(550, self.height - 570, 12))
        my_playground.append(Grave_object(600, self.height - 550, 7))
        my_playground.append(Grave_object(450, self.height - 550, 7))

        return my_playground

    def show_playground(self, game_display):
        """
        Display plaground
        @param game_display: gpygame.display
        """
        for my_obj in self.playground:
            my_obj.show(game_display)

    def create_game_object(self, game_object):
        self.game_objects.append(game_object)
        return game_object

    def show_game_objects(self, game_display):
        for game_object in self.game_objects:
            game_object.show(game_display)

    def run(self, enemy_types):
        """
        Main loop of the application
        @param enemy_types:
        """
        clock = pygame.time.Clock()
        pygame.init()
        game_display = pygame.display.set_mode((self.width, self.height))
        pygame.display.set_caption('Halloween Party')

        while True:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    quit()

            game_display.blit(self.background, [0, 0])  # Display background
            self.show_playground(game_display)          # Show playground
            self.show_game_objects(game_display)        # Show game objects

            # Default action
            for game_object in self.game_objects:
                if not game_object.dead:
                    game_object.action = 'Idle'
                else:
                    game_object.action = 'Dying'

            # Key press handling
            if not self.hero.dead:
                keys_pressed = pygame.key.get_pressed()
                if keys_pressed[pygame.K_LEFT]:
                    self.hero.set_direction(0)
                    if self.hero.can_walk(self.playground):
                        self.hero.walk(self.playground)
                if keys_pressed[pygame.K_RIGHT]:
                    self.hero.set_direction(1)
                    if self.hero.can_walk(self.playground):
                        self.hero.walk(self.playground)
                if keys_pressed[pygame.K_UP]:
                    if self.hero.isJump == 0 and self.hero.isFall == 0:
                        self.hero.isJump = 1
                if keys_pressed[pygame.K_SPACE] and not self.hero.isJump and not self.hero.isFall:
                    self.hero.isAttack = 1

                # Jumping and falling
                if self.hero.isJump:
                    self.hero.jump(self.playground)
                elif self.hero.isFall and not self.hero.isJump:
                    self.hero.fall(self.playground)

            # AI handling
            for game_object in self.game_objects:
                if not game_object.dead:
                    if game_object != self.hero:
                        if game_object.should_attack(self.hero):
                            pass
                        elif game_object.should_walk(self.playground):
                            if game_object.should_run(self.hero):
                                game_object.run()
                            else:
                                game_object.walk(self.playground)
                        elif game_object.should_wait(self.hero):
                            pass
                        else:
                            game_object.set_direction(abs(game_object.direction - 1))

                        # If hero attacks enemy will stop attacking and will be hurt
                        if game_object.isHurt:
                            game_object.hurt()
                        elif game_object.isAttack:
                            game_object.attack(self.game_objects)
                    if game_object == self.hero and not game_object.isFall and not game_object.isJump:
                        # If hero stops attacking he will be hurt
                        if game_object.isAttack:
                            game_object.attack(self.game_objects)
                        elif game_object.isHurt:
                            game_object.hurt()

            pygame.display.update()  # Update of the screen
            clock.tick(60)


if __name__ == '__main__':
    enemy_types = [3, 2, 4, 5]
    pg = Playground()
    pg.run(enemy_types)
