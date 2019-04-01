using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketNetwork.Ncs
{
    class NcsBuffer
    {
        public byte[] read_buffer;
        public byte[] write_buffer = new byte[1024];
        public int read_offset = 0;
        public int write_offset = 4;

        
        public NcsBuffer(byte[] buffer)
        {
            this.read_buffer = buffer;
        }

        public NcsBuffer(int signal, int SendTo, int space_type)
        {
            byte[] temp_buffer3 = BitConverter.GetBytes((Int16)SendTo);
            temp_buffer3.CopyTo(write_buffer, write_offset);
            write_offset += sizeof(Int16);

            byte[] temp_buffer2 = BitConverter.GetBytes((Int16)space_type);
            temp_buffer2.CopyTo(write_buffer, write_offset);
            write_offset += sizeof(Int16);
            
            byte[] temp_buffer = BitConverter.GetBytes((Int16)signal);
            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += sizeof(Int16);
        }

        public NcsBuffer()
        {

        }



        /// Write
        public void check_buffer(int size)
        {
            if (write_offset + size >= write_buffer.Length)
            {
                Array.Resize(ref write_buffer, write_buffer.Length + 1024);
            }
        }

        public void push_size()
        {
            check_buffer(sizeof(UInt32));
            byte[] temp_buffer = BitConverter.GetBytes((UInt32)write_offset);
            temp_buffer.CopyTo(write_buffer, 0);
        }

        public void push_sint16(int argument)
        {
            int size = sizeof(Int16);
            check_buffer(size);

            byte[] temp_buffer = BitConverter.GetBytes((Int16)argument);
            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += size;
        }

        public void push_uint16(int argument)
        {
            int size = sizeof(UInt16);
            check_buffer(size);

            byte[] temp_buffer = BitConverter.GetBytes((UInt16)argument);
            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += size;
        }

        public void push_sint32(int argument)
        {
            int size = sizeof(Int32);
            check_buffer(size);

            byte[] temp_buffer = BitConverter.GetBytes((Int32)argument);
            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += size;
        }

        public void push_uint32(int argument)
        {
            int size = sizeof(UInt32);
            check_buffer(size);

            byte[] temp_buffer = BitConverter.GetBytes((UInt32)argument);
            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += size;
        }

        public void push_string(string argument)
        {
            byte[] temp_buffer = Encoding.UTF8.GetBytes(argument);
            int len = temp_buffer.Length + 1;
            check_buffer(len);

            temp_buffer.CopyTo(write_buffer, write_offset);
            write_offset += len;
        }

        public byte[] get_buffer()
        {
            return write_buffer.ToArray<byte>();
        }


        
        ///  Read
        public bool pop_bool()
        {
            bool data = BitConverter.ToBoolean(read_buffer, read_offset);
            read_offset += sizeof(bool);
            return data;
        }

        public byte pop_byte()
        {
            byte data = read_buffer[read_offset];
            read_offset += sizeof(byte);
            return data;
        }

        public Int16 pop_sint16()
        {
            Int16 data = BitConverter.ToInt16(read_buffer, read_offset);
            read_offset += sizeof(Int16);
            return data;
        }

        public UInt16 pop_uint16()
        {
            UInt16 data = BitConverter.ToUInt16(read_buffer, read_offset);
            read_offset += sizeof(UInt16);
            return data;
        }

        public string pop_string()
        {
            UInt16 length = BitConverter.ToUInt16(read_buffer, read_offset);
            read_offset += sizeof(UInt16);

            string data = System.Text.Encoding.UTF8.GetString(read_buffer, read_offset, length);
            read_offset += length;

            return data;
        }
    }
}
