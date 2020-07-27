using System;

namespace perlin_noise_algorithm {

    class Vec3 {
        public float x { get; }
        public float y { get; }
        public float z { get; }

        public Vec3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    class Perlin {
        Vec3[] coordinates;
        int index;
        int length;

        float amplitude;
        float frequency;

        public Perlin(int length, float amplitude, float frequency) {

            this.length = length;
            this.amplitude = amplitude;
            this.frequency = frequency;

            // creating all the vertices
            coordinates = new Vec3[length * length];

            for (int nx = 0; nx < length; nx++) {
                for (int nz = 0; nz < length; nz++) {
                    coordinates[index] = new Vec3(nx, 4, nz);
                    index++;
                }
            }
        }

        public Vec3 noise(int x, int z) {
            Random rx = new Random();
            Random rz = new Random();

            int newX = 0;
            int newZ = 0;

            int xOffset = rx.Next(-1, 1);
            int zOffset = rz.Next(-1, 1);

            // checking if the x and z coordinates are not on the border
            if (x <= length - 1 && z <= length - 1 && x > 0 && z > 0) {
                // grabbing a new vertex to change
                newX = x + xOffset;
                newZ = z + zOffset;
            }

            // getting the origin vertex and the new vertex
            Vec3 origin = new Vec3(0, 0, 0);
            Vec3 focus = new Vec3(0, 0, 0);

            for (int i = 0; i < coordinates.Length; i++) {

                if ((int)coordinates[i].x == x && (int)coordinates[i].z == z) {
                    origin = coordinates[i];
                    Console.WriteLine("found origin");
                }
                if ((int)coordinates[i].x == newX && (int)coordinates[i].z == newZ) {
                    focus = coordinates[i];
                    Console.WriteLine("found focus");
                }
            }

            // changing the new vertex using the origin vertex
            Random randomOp = new Random();
            double rOperator = randomOp.NextDouble();

            Random ry = new Random();
            float yOffset = ry.Next(-5, 10)*frequency;

            focus = new Vec3(focus.x, origin.y + yOffset*amplitude, focus.z);
            return focus;
        }
    }

    class MainClass {

        static Perlin perlin;
        static int length = 10, index;
        static float amplitude = 1.5f;
        static float frequency = 1.234f;

        static Vec3[] coordinates;

        public static void Main(string[] args) {

            coordinates = new Vec3[length * length];
            perlin = new Perlin(length,amplitude,frequency);

            for (int x = 0; x < length; x++) {
                for (int z = 0; z < length; z++){
                    coordinates[index] = perlin.noise(x, z);
                    index++;
                }
            }

            int mappingIndex = 0;
            for (int x = 0; x < length; x++) {

                string mapping = null;
                for (int z = 0; z < length; z++) {
                    mapping += coordinates[mappingIndex].y.ToString("0.00 ");
                    mappingIndex++;
                }

                Console.WriteLine(mapping);
            }
        }
    }
}