//**************************************************************************
//
//               Demo program for labs
//
// Subject:      Computer Architectures and Parallel systems
// Author:       Petr Olivka, petr.olivka@vsb.cz, 08/2016
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

// Serial line for printf output
Serial pc( USBTX, USBRX );

#define WIDTH 16
#define HEIGHT 26
#define ENDIAN_LSB true

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
 
    Circle( Point2D t_center, int32_t t_radius, RGB t_fg, RGB t_bg ) :
        center( t_center ), radius( t_radius ), GraphElement( t_fg, t_bg ) {};
 
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
            for(int i = 0; i < HEIGHT; i++){
                for(int j = 0; j < WIDTH; j++){
                    if(font[this->character][i] & (1 << j)){
                        drawPixel(this->pos.x + j, this->pos.y + i);
                    }
                }
            }
        }
        else{
            for(int i = 0; i < HEIGHT; i++){
                int k = 0;
                for(int j = WIDTH-1; j >= 0; j--){
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
 
    Line( Point2D t_pos1, Point2D t_pos2, RGB t_fg, RGB t_bg ) :
      pos1( t_pos1 ), pos2( t_pos2 ), GraphElement( t_fg, t_bg ) {}

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
                this.pos1.x += sx;
            }
            if(e2 < dy){
                err += dx;
                this.pos1.y += sy;
            }
        }
    };
};
 
int main(){
    // Serial line initialization
    //g_pc.baud(115200);

    lcd_init();     // LCD initialization
    lcd_clear();    // LCD clear screen
 
    Point2D point;
    point.x = 5;
    point.y = 5;
    RGB fg;
    fg.r = 255;
    fg.g = 255;
    fg.b = 255;
    RGB bg;
    bg.r = 0;
    bg.g = 0;
    bg.b = 0;
    Character* ch = new Character(point, \'B\', fg, bg);
    ch->draw();
 
    Point2D point1;
    point1.x = 50;
    point1.y = 50;
    Point2D point2;
    point2.x = 100;
    point2.y = 50;
    fg.r = 255;
    fg.g = 255;
    fg.b = 255;
    bg.r = 0;
    bg.g = 0;
    bg.b = 0;
    Line* l = new Line(point1, point2, fg, bg);
    l->draw();
 
    return 0;
}