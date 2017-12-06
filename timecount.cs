using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace AutoPickingSys
{
    class timecount
    {
        /// <summary>
        /// 得到系统时钟周期的当前值
        /// </summary>
        /// <param name="lpPerformanceCount">输出参数，得到系统时钟周期的当前值</param>
        /// <returns>返回是否获取成功</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        /// <summary>
        /// 得到系统的时钟频率，每秒的周期数
        /// </summary>
        /// <param name="frequency">输出参数，得到系统的每秒周期数</param>
        /// <returns>返回是否获取成功</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryPerformanceFrequency(out long frequency);
    }
}
