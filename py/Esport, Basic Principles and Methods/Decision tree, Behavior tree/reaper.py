from wraith import Wraith
import pygame
import numpy as np


class Reaper(Wraith):
    health = 100        # Current health
    dead = False        # Current death state
    action = 'Idle'     # Current action
    actions = ['Attacking', 'Dying', 'Hurt', 'Idle', 'Running', 'Walking']     # Allowed actions
    default_direction = 1   # Default direction of images
    obj_x = 38      # Offset from left
    obj_y = 32      # Offset from top
    obj_w = 76      # Object width
    obj_h = 100     # Object height

    walk_speed = 6      # Speed of walking
    run_speed = 8       # Speed of running
    vision_dist = 320   # Distance of reaper vision
    hear_dist = 160     # Distance of reaper hearing
    attack_parts = 12   # Count of attacking frames
    can_hurt = True     # Can be hurt
    hurt_parts = 12     # Count of hurting frames

    def __init__(self, screen_width, screen_height, x, y):
        super().__init__(screen_width, screen_height, x, y, 'reaper_3')
