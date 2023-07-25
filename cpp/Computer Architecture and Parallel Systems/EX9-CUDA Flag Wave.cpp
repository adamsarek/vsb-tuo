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
#include <math.h>
 
#define PI 3.14159265
 
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
 
__global__ void kernel_flag_wave( CudaPic t_cuda_pic )
{
    // X,Y coordinates
    int l_y = blockDim.y * blockIdx.y + threadIdx.y;
    int l_x = blockDim.x * blockIdx.x + threadIdx.x;
    if ( l_x >= t_cuda_pic.m_size.x ) return;
    if ( l_y >= t_cuda_pic.m_size.y ) return;
 
    double param, result;
    param = l_x % 180;
    result = sin(param*PI/180);
    int diff = 50;
    int sinDiff = result * diff;
 
    // Point [l_x,l_y] selection from image
    uchar3 l_bgr = t_cuda_pic.m_p_uchar3[ (l_y+sinDiff) * t_cuda_pic.m_size.x + l_x ];
 
    // Store point [l_x,l_y] back to image
    t_cuda_pic.m_p_uchar3[ (l_y) * t_cuda_pic.m_size.x + l_x ] = l_bgr;
}
 
void cu_create_flag( CudaPic t_pic, uint2 t_block_size )
{
    cudaError_t l_cerr;
 
    // Grid creation with computed organization
    dim3 l_grid( ( t_pic.m_size.x + t_block_size.x - 1 ) / t_block_size.x,
                 ( t_pic.m_size.y + t_block_size.y - 1 ) / t_block_size.y );
    kernel_flag<<< l_grid, dim3( t_block_size.x, t_block_size.y ) >>>( t_pic );
    kernel_flag_wave<<< l_grid, dim3( t_block_size.x, t_block_size.y ) >>>( t_pic );
 
    if ( ( l_cerr = cudaGetLastError() ) != cudaSuccess )
        printf( \"CUDA Error [%d] - \'%s\'\\n\", __LINE__, cudaGetErrorString( l_cerr ) );
 
    cudaDeviceSynchronize();
 
}