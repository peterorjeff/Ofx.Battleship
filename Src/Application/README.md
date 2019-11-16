# Application Layer

This layer contains all application logic.

## Ship Collision Detection Logic

When adding a ship, collision detection is required before placing a new ship.
The linq expression used to determine this has a couple of steps that warrent explanation with examples.

```
var xList = request.ShipParts.Select(x => x.X).Distinct();
var yList = request.ShipParts.Select(x => x.Y).Distinct();

var collisions = _context.Ships
    .Include(ship => ship.ShipParts)
    .Where(ship => ship.Board == board)
    .SelectMany(ship => ship.ShipParts, (ship, parts) => new
    {
        parts.X,
        parts.Y
    })
    .Where(part => xList.Contains(part.X) && yList.Contains(part.Y))
    .AsNoTracking();
```

The `xList` and `yList` collections are derived from the add ship request, and we distinct them as one or the other will always contain entirely duplicate values.
Example request:

```
1,1
1,2
1,3
```

Will result in the following list values:

```
xlist = 1
ylist = 1,2,3
```

With the following existing ships:

```
1,1
2,1
3,1
```

The collisions where cluase results in the following evaluations:

```
where xlist contains 1 && ylist contains 1 = true
where xlist contains 2 && ylist contains 1 = false
where xlist contains 3 && ylist contains 1 = false
```
