// ***********************************************************************
//
// Demo program for education in subject
// Computer Architectures and Paralel Systems.
// Petr Olivka, dep. of Computer Science, FEI, VSB-TU Ostrava
// email:petr.olivka@vsb.cz
//
// Example of CUDA Technology Usage with unified memory.
//
// Manipulation with prepared image.
//
// ***********************************************************************
 
#include <cuda_device_runtime_api.h>
#include <cuda_runtime.h>
#include <stdio.h>
#include \"pic_type.h\"
 
// Every threads identifies its position in grid and in block and modify image
__global__ void kernel_flag( CudaPic t_cuda_pic )
{
    // X,Y coordinates 
    int l_y = blockDim.y * blockIdx.y + threadIdx.y;
    int l_x = blockDim.x * blockIdx.x + threadIdx.x;
    if ( l_x >= t_cuda_pic.m_size.x ) return;
    if ( l_y >= t_cuda_pic.m_size.y ) return;
 
    // Point [l_x,l_y] selection from image
    uchar3 l_bgr = t_cuda_pic.m_p_uchar3[ l_y * t_cuda_pic.m_size.x + l_x ];
 
    // White stripes (BGR format)
    if((l_x > t_cuda_pic.m_size.x / 25 * 7 && l_x < t_cuda_pic.m_size.x / 25 * 11)
    || (l_y > t_cuda_pic.m_size.y / 18 * 7 && l_y < t_cuda_pic.m_size.y / 18 * 11)){
        l_bgr.x = l_bgr.y = l_bgr.z = 255;
    }
    // Red stripes (BGR format)
    if((l_x > t_cuda_pic.m_size.x / 25 * 8 && l_x < t_cuda_pic.m_size.x / 25 * 10)
    || (l_y > t_cuda_pic.m_size.y / 18 * 8 && l_y < t_cuda_pic.m_size.y / 18 * 10)){
        l_bgr.x = 53;
        l_bgr.y = 30;
        l_bgr.z = 220;
    }
 
    // Store point [l_x,l_y] back to image
    t_cuda_pic.m_p_uchar3[ l_y * t_cuda_pic.m_size.x + l_x ] = l_bgr;
}
 
void cu_create_flag( CudaPic t_pic, uint2 t_block_size )
{
    cudaError_t l_cerr;
 
    // Grid creation with computed organization
    dim3 l_grid( ( t_pic.m_size.x + t_block_size.x - 1 ) / t_block_size.x,
                 ( t_pic.m_size.y + t_block_size.y - 1 ) / t_block_size.y );
    kernel_flag<<< l_grid, dim3( t_block_size.x, t_block_size.y ) >>>( t_pic );
 
    if ( ( l_cerr = cudaGetLastError() ) != cudaSuccess )
        printf( \"CUDA Error [%d] - \'%s\'\\n\", __LINE__, cudaGetErrorString( l_cerr ) );
 
    cudaDeviceSynchronize();
 
}
 
// ***********************************************************************
//
// Demo program for education in subject
// Computer Architectures and Parallel Systems.
// Petr Olivka, dep. of Computer Science, FEI, VSB-TU Ostrava
// email:petr.olivka@vsb.cz
//
// Example of CUDA Technology Usage without unified memory.
//
// Image creation and its modification using CUDA.
// Image manipulation is performed by OpenCV library. 
//
// ***********************************************************************
 
#include <stdio.h>
#include <cuda_device_runtime_api.h>
#include <cuda_runtime.h>
#include <opencv2/opencv.hpp>
 
#include \"uni_mem_allocator.h\"
#include \"pic_type.h\"
 
// Prototype of function in .cu file
void cu_create_flag( CudaPic t_pic, uint2 t_block_size );
 
// Image size
#define SIZEX 500 // Width of image
#define SIZEY 350 // Height of image
// Block size for threads
#define BLOCKX 10 // block width
#define BLOCKY 10 // block height
 
int main()// Demo program for education in subject
 
#include <stdio.h>
#include <cuda_device_runtime_api.h>
#include <cuda_runtime.h>
#include <opencv2/opencv.hpp>
 
#include \"uni_mem_allocator.h\"
#include \"pic_type.h\"
 
// Prototype of function in .cu file
void cu_create_flag( CudaPic t_pic, uint2 t_block_size );
 
// Image size
#define SIZEX 500 // Width of image
#define SIZEY 350 // Height of image
// Block size for threads
#define BLOCKX 10 // block width
#define BLOCKY 10 // block height
 
int main()
{
    // Uniform Memory allocator for Mat
    UniformAllocator allocator;
    cv::Mat::setDefaultAllocator( &allocator );
 
    // Creation of empty image.
    // Image is stored line by line.
    cv::Mat l_cv_img( SIZEY, SIZEX, CV_8UC3 );
 
    // Image filling by background color
    for ( int y = 0; y < l_cv_img.rows; y++ )
        for ( int x  = 0; x < l_cv_img.cols; x++ )
        {
            // Flag background (BGR)
            uchar3 l_bgr = ( uchar3 ) { 156, 82, 2 };
 
            // put pixel into image
            cv::Vec3b l_v3bgr( l_bgr.x, l_bgr.y, l_bgr.z );
            l_cv_img.at<cv::Vec3b>( y, x ) = l_v3bgr;
            // also possible: cv_img.at<uchar3>( y, x ) = bgr;
        }
 
    CudaPic l_pic_img;
    l_pic_img.m_size.x = l_cv_img.size().width; // equivalent to cv_img.cols
    l_pic_img.m_size.y = l_cv_img.size().height; // equivalent to cv_img.rows
    l_pic_img.m_p_uchar3 = ( uchar3* ) l_cv_img.data;
 
    // Function calling from .cu file
    uint2 l_block_size = { BLOCKX, BLOCKY };
    cu_create_flag( l_pic_img, l_block_size );
 
    // Show flag image
    cv::imshow( \"Vlajka Islandu\", l_cv_img );
    cv::waitKey( 0 );
}
 
 
{
    // Uniform Memory allocator for Mat
    UniformAllocator allocator;
    cv::Mat::setDefaultAllocator( &allocator );
 
    // Creation of empty image.
    // Image is stored line by line.
    cv::Mat l_cv_img( SIZEY, SIZEX, CV_8UC3 );
 
    // Image filling by background color
    for ( int y = 0; y < l_cv_img.rows; y++ )
        for ( int x  = 0; x < l_cv_img.cols; x++ )
        {
            // Flag background (BGR)
            uchar3 l_bgr = ( uchar3 ) { 156, 82, 2 };
 
            // put pixel into image
            cv::Vec3b l_v3bgr( l_bgr.x, l_bgr.y, l_bgr.z );
            l_cv_img.at<cv::Vec3b>( y, x ) = l_v3bgr;
            // also possible: cv_img.at<uchar3>( y, x ) = bgr;
        }
 
    CudaPic l_pic_img;
    l_pic_img.m_size.x = l_cv_img.size().width; // equivalent to cv_img.cols
    l_pic_img.m_size.y = l_cv_img.size().height; // equivalent to cv_img.rows
    l_pic_img.m_p_uchar3 = ( uchar3* ) l_cv_img.data;
 
    // Function calling from .cu file
    uint2 l_block_size = { BLOCKX, BLOCKY };
    cu_create_flag( l_pic_img, l_block_size );
 
    // Show flag image
    cv::imshow( \"Vlajka Islandu\", l_cv_img );
    cv::waitKey( 0 );
}