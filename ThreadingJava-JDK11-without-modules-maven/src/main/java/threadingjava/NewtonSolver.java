package threadingjava;

import threadingjava.math.Polynome;
import threadingjava.math.Complex;
import java.awt.Color;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class NewtonSolver extends AlgorithmBase {

    private final double xmin = -1.5;
    private final double xmax = 1.5;
    private final double ymin = -1.5;
    private final double ymax = 1.5;

    private final double xstep = (xmax - xmin) / width;
    private final double ystep = (ymax - ymin) / height;

    private final List<Complex> roots = Collections.synchronizedList(new ArrayList<>());
    private final Polynome p = new Polynome();
    private final Polynome pd;

    private final Color[] colours = new Color[]{
        Color.red, Color.blue, Color.green, Color.yellow, Color.orange, Color.magenta, Color.pink, Color.cyan, Color.gray
    };

    private final Color[][] iraw = new Color[height][width];

    public NewtonSolver() {
        List<Complex> coef = p.getCoefficients();
        coef.add(new Complex(1, 0));
        coef.add(new Complex(0, 0));
        coef.add(new Complex(0, 0));
        coef.add(new Complex(1, 0));

        pd = p.derive();
    }

    @Override
    public void solvePoint(int j, int i) {
        // find "world" coordinates of pixel
        final double x = xmin + j * xstep;
        final double y = ymin + i * ystep;
        Complex ox = createComplexNumber(x, y);

        // find solution of equation using newton's iteration
        float it = 0;
        for (int q = 0; q < 30; q++) {
            Complex diff = p.eval(ox).divide(pd.eval(ox));
            ox = ox.subtract(diff);

            if (diff.getRe() * diff.getRe() + diff.getIm() * diff.getIm() >= 0.5) {
                q--;
            }
            it++;
        }

        // find solution root number
        boolean known = false;
        int id = findRootId(ox, known, 0);

        Color vv = createColor(id, it);

        setColor(j, i, vv);
    }

    private Complex createComplexNumber(double x, double y) {
        Complex ox = new Complex(x, y);
        if (ox.getRe() == 0) {
            ox.setRe(0.0001);
        }
        if (ox.getIm() == 0) {
            ox.setIm(0.0001);
        }
        return ox;
    }

    private void setColor(int j, int i, Color vv) {
        image.setRGB(j, i, vv.getRGB());
    }

    private Color createColor(int id, float it) {
        Color vv = colours[id % colours.length];
        vv = new Color(Math.min(Math.max(0, vv.getRed() - (int) it * 4), 255), Math.min(Math.max(0, vv.getGreen() - (int) it * 4), 255), Math.min(Math.max(0, vv.getBlue() - (int) it * 4), 255));
        return vv;
    }

    private int findRootId(Complex ox, boolean known, int id) {
        for (int w = 0; w < roots.size(); w++) {
            if (Math.pow(ox.getRe() - roots.get(w).getRe(), 2) + Math.pow(ox.getIm() - roots.get(w).getIm(), 2) <= 0.01) {
                known = true;
                id = w;
            }
        }
        if (!known) {
            roots.add(ox);
            id = roots.size();
        }
        return id;
    }
}
