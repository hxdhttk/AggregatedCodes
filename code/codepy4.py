class MultiplyGate(object):
    def forward(self, x, y):
        z = x * y
        self.x = x
        self.y = y
        return z
    def backword(self, dz):
        dx = self.y * dz
        dy = self.x * dz
        return [ dx, dy ]

class AddGate(object):
    def forward(self, x, y):
        z = x + y
        self.x = x
        self.y = y
        return z
    def backword(self, dz):
        dx = 1 * dz
        dy = 1 * dz
        return [ dx, dy ]

gateMul = MultiplyGate()
gateAdd = AddGate()

gateAdd.forward(5, 6)
gateMul.forward(2, 3)

gateMul.backword(2)
gateAdd.backword(3)