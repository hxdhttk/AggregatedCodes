"""Softmax."""

import numpy as np

scores = [3.0, 1.0, 0.2]

def softmax(x: np.array):
    return np.exp(x) / np.sum(np.exp(x), axis=0)

print(softmax(scores))

# Plot softmax curves
import matplotlib.pyplot as plt
x = np.arange(-2.0, 6.0, 0.1)
#scores = np.vstack([x, 10 * np.ones_like(x), 100 * np.ones_like(x)])

#plt.plot(x, softmax(scores).T, linewidth=2)
#plt.legend(['1.0x', '10.0x', '100.0x'])
#plt.show()
scores = np.vstack([x, np.ones_like(x) / 10.0, np.ones_like(x) / 100.0 ])
plt.plot(x, softmax(scores).T, linewidth=2)
plt.legend(['1.0x', '0.1x', '0.01x'])
plt.show()