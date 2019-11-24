package threadingjava;

import java.awt.Color;

public class Mandelbrot extends AlgorithmBase {

    private final double mandelXMinimum = -2.5;
    private final double mandelXMaximum = 1;
    private final double mandelYMinimum = -1;
    private final double mandelYMaximum = 1;

    private final Color[] pallette;

    public Mandelbrot() {
        this.pallette = new Color[]{
            Color.white, Color.black
        };
    }

    public static Color interpolate(Color f, Color t, double p) {
        int red = (int) Math.round(p * f.getRed() + (1 - p) * t.getRed());
        int green = (int) Math.round(p * f.getGreen() + (1 - p) * t.getGreen());
        int blue = (int) Math.round(p * f.getBlue() + (1 - p) * t.getBlue());

        red = Math.max(0, Math.min(255, red));
        green = Math.max(0, Math.min(255, green));
        blue = Math.max(0, Math.min(255, blue));

        return new Color(red, green, blue);
    }

    @Override
    public void solvePoint(int xp, int yp) {
        double x0 = (((double) xp) / width) * (mandelXMaximum - mandelXMinimum) + mandelXMinimum;
        double y0 = (((double) yp) / height) * (mandelYMaximum - mandelYMinimum) + mandelYMinimum;

        double x = 0.;
        double y = 0.;
        double iteration = 0;
        int maxIterations = 1000;

        while (x * x + y * y < (1 << 16) && iteration < maxIterations) {
            double xtemp = x * x - y * y + x0;
            y = 2 * x * y + y0;
            x = xtemp;
            iteration++;
        }
        if (iteration < maxIterations) {
            double logzn = Math.log10(x * x + y * y) / 2;
            double nu = Math.log10(logzn / Math.log10(2)) / Math.log10(2);
            iteration = iteration + 1 - nu;
        }

        Color c1 = pallette[((int) Math.floor(iteration)) % pallette.length];
        Color c2 = pallette[((int) Math.floor(iteration) + 1) % pallette.length];

        double t = iteration - Math.floor(iteration);
        Color c = interpolate(c1, c2, t);

        image.setRGB(xp, yp, c.getRGB());
    }

}
