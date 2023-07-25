import pygame
import numpy as np
import pygame.gfxdraw
import os
import math


class My_Object:
    frame_id = 0
    frames = {}         # Object with all action frames for certain character

    fall_id = 0
    fall_parts = 10     # Speed of falling
    isFall = 0          # Defines if character is falling
    jump_id = 0
    jump_parts = 20     # Speed of jumping
    isJump = 0          # Defines if character is jumping
    attack_id = 0
    isAttack = 0        # Defines if character is attacking
    hurt_id = 0
    isHurt = 0          # Defines if character is being hurt

    def __init__(self, screen_width, screen_height, x, y, type):
        self.type = type
        self.load()

        self.screen_width = screen_width                                # Screen width
        self.screen_height = screen_height                              # Screen height
        self.w = self.frames[self.action][self.frame_id].get_width()    # Width of image
        self.h = self.frames[self.action][self.frame_id].get_height()   # Height of image
        self.x = x                                                      # X of image
        self.y = y - self.obj_y - self.obj_h                            # Y of image
        self.col_l = self.x + self.obj_x                                # Collision left of object
        self.col_r = self.x + self.obj_x + self.obj_w                   # Collision right of object
        self.col_t = self.y + self.obj_y                                # Collision top of object
        self.col_b = self.y + self.obj_y + self.obj_h                   # Collision bottom of object
        self.direction = self.default_direction                         # Direction of character
        self.jump_h = self.obj_h * 2                                    # Jump height
        self.fall_h = self.obj_h * 2                                    # Fall height before reaching constant speed

    # Loading all action frames for predefined paths
    def load(self):
        self.frames = {}
        for action in self.actions:
            path = self.type + '/' + action
            if os.path.exists(path):
                self.frames[action] = []
                for file in os.listdir(path):
                    if file.endswith('.png'):
                        imgPath = os.path.join(path, file)
                        self.frames[action].append(pygame.image.load(imgPath))

    # Shows character frames on screen with right action and direction
    def show(self, game_display):
        if self.frame_id + 1 >= len(self.frames[self.action]):
            if not self.dead:
                self.frame_id = 0
        else:
            self.frame_id += 1
        if self.direction == self.default_direction:
            self.frame = self.frames[self.action][self.frame_id]
        else:
            frame = self.frames[self.action][self.frame_id]
            self.frame = pygame.transform.flip(frame, True, False)

        game_display.blit(self.frame, (self.x, self.y))

    # Sets direction of character
    def set_direction(self, direction):
        if self.direction != direction:
            self.obj_x = self.w - (self.obj_x + self.obj_w)
            self.col_l = self.x + self.obj_x
            self.col_r = self.x + self.obj_x + self.obj_w
            self.direction = direction

    # Tells if character can walk or otherwise he would hit a wall
    def can_walk(self, playground):
        if self.direction == 0:
            if self.col_l > 0:
                for pg_object in playground:
                    if pg_object.type == 'tile_block':
                        for tile in pg_object.block:
                            if self.col_l <= tile.x + tile.img.get_width() and self.col_r > tile.x + tile.img.get_width() and (self.col_t < tile.y + tile.img.get_height() and self.col_b > tile.y):
                                return False
                return True
            else:
                return False
        elif self.direction == 1:
            if self.col_r < self.screen_width:
                for pg_object in playground:
                    if pg_object.type == 'tile_block':
                        for tile in pg_object.block:
                            if self.col_r >= tile.x and self.col_l < tile.x and (self.col_t < tile.y + tile.img.get_height() and self.col_b > tile.y):
                                return False
                return True
            else:
                return False

    # Tells if character should walk or otherwise he would hit a wall or crate or he would fall down
    def should_walk(self, playground):
        can_walk = self.can_walk(playground)
        if not can_walk:
            return False

        if self.direction == 0:
            if self.col_l > 0:
                for pg_object in playground:
                    if pg_object.type == 'tile_block' or pg_object.type == 'crate':
                        if pg_object.type == 'crate':
                            pg_object.block = [pg_object]

                        for tile in pg_object.block:
                            if self.col_l <= tile.x + tile.img.get_width() and self.col_r > tile.x + tile.img.get_width() and self.col_t < tile.y + tile.img.get_height() and self.col_b > tile.y:
                                return False

                        # Preventing falling down
                        for i in range(len(pg_object.block)):
                            if self.col_b >= pg_object.block[i].y and self.col_t <= pg_object.block[i].y:
                                if self.col_l <= pg_object.block[i].x and self.col_r >= pg_object.block[i].x:
                                    return False
                                break
                return True
            else:
                return False
        elif self.direction == 1:
            if self.col_r < self.screen_width:
                for pg_object in playground:
                    if pg_object.type == 'tile_block' or pg_object.type == 'crate':
                        if pg_object.type == 'crate':
                            pg_object.block = [pg_object]

                        for tile in pg_object.block:
                            if self.col_r >= tile.x and self.col_l < tile.x and (self.col_t < tile.y + tile.img.get_height() and self.col_b > tile.y):
                                return False

                        # Preventing falling down
                        for i in range(len(pg_object.block)-1, 0, -1):
                            if self.col_b >= pg_object.block[i].y and self.col_t <= pg_object.block[i].y:
                                if self.col_r >= pg_object.block[i].x + pg_object.block[i].img.get_width() and self.col_l <= pg_object.block[i].x + pg_object.block[i].img.get_width():
                                    return False
                                break
                return True
            else:
                return False

    # Character will walk at given walk_speed and fall if no tiles are below him
    def walk(self, playground):
        self.action = 'Walking'

        if self.direction == 0:
            self.x -= self.walk_speed
            self.col_l -= self.walk_speed
            self.col_r -= self.walk_speed
        elif self.direction == 1:
            self.x += self.walk_speed
            self.col_l += self.walk_speed
            self.col_r += self.walk_speed

        self.isFall = 1
        for pg_object in playground:
            if pg_object.type == 'tile_block' or pg_object.type == 'crate':
                if pg_object.type == 'crate':
                    pg_object.block = [pg_object]

                for tile in pg_object.block:
                    if self.col_b >= tile.y and self.col_t < tile.y and (self.col_l < tile.x + tile.img.get_width() and self.col_r > tile.x):
                        self.isFall = 0
                        break
                        break

    # Tells if reaper should wait when he sees hero on different island while he himself reaches end of his island
    def should_wait(self, hero):
        if self.type == 'reaper_3' and not hero.dead:
            if abs((self.col_l + self.obj_w * 0.5) - (hero.col_l + hero.obj_w * 0.5)) - self.obj_w * 0.5 - hero.obj_w * 0.5 <= self.hear_dist and (self.col_t < hero.col_b and self.col_b > hero.col_t):
                return True
            elif abs((self.col_l + self.obj_w * 0.5) - (hero.col_l + hero.obj_w * 0.5)) - self.obj_w * 0.5 - hero.obj_w * 0.5 <= self.vision_dist and (self.direction == 0 and self.col_l >= hero.col_r or self.direction == 1 and self.col_r <= hero.col_l) and (self.col_t < hero.col_b and self.col_b > hero.col_t):
                return True
            else:
                return False
        else:
            return False

    # Tells if reaper should run when he hears or sees hero within certain distance
    def should_run(self, hero):
        if self.type == 'reaper_3' and not hero.dead:
            if abs((self.col_l + self.obj_w * 0.5) - (hero.col_l + hero.obj_w * 0.5)) - self.obj_w * 0.5 - hero.obj_w * 0.5 <= self.hear_dist and (self.col_t < hero.col_b and self.col_b > hero.col_t):
                if self.col_l >= hero.col_r and self.direction == 1 or self.col_r <= hero.col_l and self.direction == 0:
                    self.set_direction(abs(self.direction - 1))
                return True
            elif abs((self.col_l + self.obj_w * 0.5) - (hero.col_l + hero.obj_w * 0.5)) - self.obj_w * 0.5 - hero.obj_w * 0.5 <= self.vision_dist and (self.direction == 0 and self.col_l >= hero.col_r or self.direction == 1 and self.col_r <= hero.col_l) and (self.col_t < hero.col_b and self.col_b > hero.col_t):
                return True
            else:
                return False
        else:
            return False

    # Character will run at given run_speed
    def run(self):
        self.action = 'Running'

        if self.direction == 0:
            self.x -= self.run_speed
            self.col_l -= self.run_speed
            self.col_r -= self.run_speed
        elif self.direction == 1:
            self.x += self.run_speed
            self.col_l += self.run_speed
            self.col_r += self.run_speed

    # Character will jump until he reaches certain height or another tile from underneath
    def jump(self, playground):
        if self.jump_id < self.jump_parts:
            jump_last = 0
            if self.jump_id > 0:
                jump_last = math.sin(self.jump_id * (math.pi * 0.5) / self.jump_parts)
            jump_done = math.sin((self.jump_id + 1) * (math.pi * 0.5) / self.jump_parts)
            jump_step = self.jump_h * (jump_done - jump_last)

            self.y -= jump_step
            self.col_t -= jump_step
            self.col_b -= jump_step

            self.jump_id += 1

            # Preventing jumping through underneath of tiles
            for pg_object in playground:
                if pg_object.type == 'tile_block':
                    for tile in pg_object.block:
                        if self.col_t <= tile.y + tile.img.get_height() and self.col_b > tile.y + tile.img.get_height() and (self.col_l < tile.x + tile.img.get_width() and self.col_r > tile.x):
                            self.y += tile.y + tile.img.get_height() - self.col_t
                            self.col_b += tile.y + tile.img.get_height() - self.col_t
                            self.col_t += tile.y + tile.img.get_height() - self.col_t

                            self.isFall = 1
                            self.isJump = 0
                            self.jump_id = 0
                            break
                            break
        else:
            self.isFall = 1
            self.isJump = 0
            self.jump_id = 0

    # Character will fall until check for stop or falling will pass
    def fall(self, playground):
        if self.fall_id < 1:
            fall_last = math.sin((self.fall_parts * 0.5 - 1) * (math.pi * 0.5) / (self.fall_parts * 2))
            fall_done = math.sin((self.fall_parts * 0.5) * (math.pi * 0.5) / (self.fall_parts * 2))
            fall_step = self.fall_h * (fall_done - fall_last)

            self.y += fall_step
            self.col_t += fall_step
            self.col_b += fall_step

            self.fall_stop_check(playground)

    # Stops falling of character
    def fall_stop_check(self, playground):
        for pg_object in playground:
            if pg_object.type == 'tile_block' or pg_object.type == 'crate':
                if pg_object.type == 'crate':
                    pg_object.block = [pg_object]

                for tile in pg_object.block:
                    if self.col_b >= tile.y and self.col_t < tile.y and (self.col_l < tile.x + tile.img.get_width() and self.col_r > tile.x):
                        self.y -= self.col_b - tile.y
                        self.col_t -= self.col_b - tile.y
                        self.col_b -= self.col_b - tile.y

                        self.isFall = 0
                        self.fall_id = 0
                        break
                        break

    # Tells if enemy should attack hero which is true when hero is not dead, is idle and also he meets the enemy
    def should_attack(self, hero):
        if ((self.col_r >= hero.col_l and self.col_l < hero.col_l) or (self.col_l <= hero.col_r and self.col_r > hero.col_l)) and (self.col_t < hero.col_b and self.col_b > hero.col_t):
            if not hero.dead:
                if hero.action == 'Idle':
                    self.isAttack = 1
                    return True
        return False

    # Character will attack and hurt the other character
    def attack(self, game_objects):
        self.action = 'Attacking'
        if self.attack_id < self.attack_parts:
            self.attack_id += 1

            if self.type == 'hero':
                for game_object in game_objects:
                    if not game_object.dead and game_object.type != 'hero' and game_object.col_r >= self.col_l and game_object.col_l <= self.col_r and game_object.col_t <= self.col_b and game_object.col_b >= self.col_t:
                        game_object.isHurt = 1
                        break
            else:
                for game_object in game_objects:
                    if not game_object.dead and game_object.type == 'hero':
                        game_object.isHurt = 1
                        break
        else:
            self.isAttack = 0
            self.attack_id = 0

    # Character will be hurt until he dies
    def hurt(self):
        if self.can_hurt:
            self.action = 'Hurt'
            if self.hurt_id < self.hurt_parts:
                self.hurt_id += 5
            else:
                self.health -= 2
                if self.health <= 0:
                    self.dead = True

                self.isHurt = 0
                self.hurt_id = 0
