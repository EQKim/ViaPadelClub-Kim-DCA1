# Player Directory View Sketch

```text
+-------------------------------------------------------------+
| Player Directory                                      [VIP] |
+-------------------------------------------------------------+
| Filters: [All players] [VIP only] [Banned only]              |
+-------------------------------------------------------------+
| University                  | VIP | Banned | Player Id       |
|-----------------------------|-----|--------|-----------------|
| Assignment 8 VIA            | Yes | No     | ...             |
| Assignment 8 AU             | No  | Yes    | ...             |
+-------------------------------------------------------------+
```

## Description

The player directory view is intended for club staff.
It gives a compact overview of players and allows filtering by VIP or banned status.

The query returns only read-model data needed by the view:

* player id
* university name
* VIP status
* banned status
