//**************************************************************************
//
//               Demo program for labs
//
// Subject:      Computer Architectures and Parallel systems
// Author:       Petr Olivka, petr.olivka@vsb.cz, 09/2019
// Organization: Department of Computer Science, FEECS,
//               VSB-Technical University of Ostrava, CZ
//
// File:         LCD module
// Student ID:   SAR0083
//
//**************************************************************************

#include \"mbed.h\"
#include \"lcd_lib.h\"
#include \"font16x26_lsb.h\"

#define FONT_WIDTH 16
#define FONT_HEIGHT 26
#define ENDIAN_LSB true

#define LCD_WIDTH       320
#define LCD_HEIGHT      240

struct Point2D
{
    int32_t x, y;
};

struct RGB
{
    uint8_t r, g, b;
};

class GraphElement
{
public:
    // foreground and background color
    RGB fg_color, bg_color;

    // constructor
    GraphElement( RGB t_fg_color, RGB t_bg_color ) :
        fg_color( t_fg_color ), bg_color( t_bg_color ) {}

    // ONLY ONE INTERFACE WITH LCD HARDWARE!!!
    void drawPixel( int32_t t_x, int32_t t_y ) { lcd_put_pixel( t_x, t_y, convert_RGB888_to_RGB565( fg_color ) ); }

    // Draw graphics element
    virtual void draw() = 0;

    // Hide graphics element
    virtual void hide() { swap_fg_bg_color(); draw(); swap_fg_bg_color(); }
private:
    // swap foreground and backgroud colors
    void swap_fg_bg_color() { RGB l_tmp = fg_color; fg_color = bg_color; bg_color = l_tmp; }

    // IMPLEMENT!!!
    // conversion of 24-bit RGB color into 16-bit color format
    int convert_RGB888_to_RGB565( RGB t_color ) {
        t_color.r = t_color.r >> 3;
        t_color.g = t_color.g >> 2;
        t_color.b = t_color.b >> 3;
        return (t_color.r << 11 | t_color.g << 5 | t_color.b);
    }
};


class Pixel : public GraphElement
{
public:
    // constructor
    Pixel( Point2D t_pos, RGB t_fg_color, RGB t_bg_color ) : pos( t_pos ), GraphElement( t_fg_color, t_bg_color ) {}
    // Draw method implementation
    virtual void draw() { drawPixel( pos.x, pos.y ); }
    // Position of Pixel
    Point2D pos;
};


class Circle : public GraphElement
{
public:
    // Center of circle
    Point2D center;
    // Radius of circle
    int32_t radius;

    Circle( Point2D t_center, int32_t t_radius, RGB t_fg, RGB t_bg ) : center( t_center ), radius( t_radius ), GraphElement( t_fg, t_bg ) {};

    // IMPLEMENT!!!
    void draw() {
        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int err = dx - (radius * 2);

        while(x >= y){
            drawPixel(this->center.x + x, this->center.y + y);
            drawPixel(this->center.x + y, this->center.y + x);
            drawPixel(this->center.x - y, this->center.y + x);
            drawPixel(this->center.x - x, this->center.y + y);
            drawPixel(this->center.x - x, this->center.y - y);
            drawPixel(this->center.x - y, this->center.y - x);
            drawPixel(this->center.x + y, this->center.y - x);
            drawPixel(this->center.x + x, this->center.y - y);

            if(err <= 0){
                y++;
                err += dy;
                dy += 2;
            }
            if(err > 0){
                x--;
                dx += 2;
                err += (-radius * 2) + dx;
            }
        }
    }
};

class Character : public GraphElement
{
public:
    // position of character
    Point2D pos;
    // character
    char character;

    Character( Point2D t_pos, char t_char, RGB t_fg, RGB t_bg ) : pos( t_pos ), character( t_char ), GraphElement( t_fg, t_bg ) {};

    // IMPLEMENT!!!
    void draw() {
        if(ENDIAN_LSB){
            for(int i = 0; i < FONT_HEIGHT; i++){
                for(int j = 0; j < FONT_WIDTH; j++){
                    if(font[this->character][i] & (1 << j)){
                        drawPixel(this->pos.x + j, this->pos.y + i);
                    }
                }
            }
        }
        else{
            for(int i = 0; i < FONT_HEIGHT; i++){
                int k = 0;
                for(int j = FONT_WIDTH-1; j >= 0; j--){
                    if(font[this->character][i] & (1 << j)){
                        drawPixel(this->pos.x + k, this->pos.y + i);
                    }
                    k++;
                }
            }
        }
    };
};

class Line : public GraphElement
{
public:
    // the first and the last point of line
    Point2D pos1, pos2;

    Line( Point2D t_pos1, Point2D t_pos2, RGB t_fg, RGB t_bg ) : pos1( t_pos1 ), pos2( t_pos2 ), GraphElement( t_fg, t_bg ) {}

    // IMPLEMENT!!!
    void draw() {
        int dx = abs(this->pos2.x - this->pos1.x);
        int sx = (this->pos1.x < this->pos2.x ? 1 : -1);
        int dy = abs(this->pos2.y - this->pos1.y);
        int sy = (this->pos1.y < this->pos2.y ? 1 : -1);
        int err = (dx > dy ? dx : -dy) / 2;
        int e2;

        while(1){
            drawPixel(this->pos1.x, this->pos1.y);
            if(this->pos1.x == this->pos2.x && this->pos1.y == this->pos2.y){ break; }

            e2 = err;
            if(e2 > -dx){
                err -= dy;
                this->pos1.x += sx;
            }
            if(e2 < dy){
                err += dx;
                this->pos1.y += sy;
            }
        }
    };
};

void analog_clock_hand(Point2D point, int radius, int length, int angle_step, RGB fg, RGB bg, double time){
    double PI = acos(-1);
    Point2D point_hand;

    point_hand.x = point.x + (int)((radius / 100 * length) * sin((time * angle_step * PI) / 180));
    point_hand.y = point.y - (int)((radius / 100 * length) * cos((time * angle_step * PI) / 180));

    Line* hand = new Line(point, point_hand, fg, bg);
    hand->draw();
}

void analog_clock(Point2D point, int radius, int* hand_lengths, RGB* hand_colors, double* time, RGB fg, RGB bg){
    Circle* circle = new Circle(point, radius, fg, bg);
    circle->draw();
    analog_clock_hand(point, radius, hand_lengths[0], 30, hand_colors[0], bg, time[0] + time[1] / 60 + time[2] / 3600);
    analog_clock_hand(point, radius, hand_lengths[1], 6, hand_colors[1], bg, time[1] + time[2] / 60);
    analog_clock_hand(point, radius, hand_lengths[2], 6, hand_colors[2], bg, time[2]);
}

int main()
{
    lcd_init();                     // LCD initialization

    lcd_clear();                    // LCD clear screen

    Point2D point = {LCD_WIDTH/2, LCD_HEIGHT/2};
    int radius = 100;
    int hand_lengths [3] = {60, 75, 90};
    RGB hand_colors [3] = {
        {255, 255, 255},
        {255, 255, 255},
        {255, 0, 0}
    };
    double time [3] = {0, 45, 30};
    RGB fg = {255, 255, 255};
    RGB bg = {0, 0, 0};
    analog_clock(point, radius, hand_lengths, hand_colors, time, fg, bg);

    return 0;
}