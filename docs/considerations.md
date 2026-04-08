## Considerations
- Tried multithreading; performance was slower due to overhead from thread scheduling and task management.
    - This was expected, as the images are very small.
- Note A: the signal function and counter could be combined by simply counting whenever the threshold is met and prev is 0 (prev tracked in the outer loop).
    - However, keeping them separate seemed cleaner architecturally. This allows other potential processes to use the signal independently and does not introduce significant overhead.
- Note B: During optimization, an edge case in img_4 was discovered where counting black pixels with a minimum threshold could overcount vertical lines. 
    as one column appears to only have one black pixel triggering an extra transition -> theshold was set to anything > 1
    The fix was to simplify the check: a column is considered a black bar if **any black pixel** exists in the top half. 
    This early break and simplification works correctly as:
    - Lines are vertically symmetrical
    - Background is pure white
    - Lines are continuous and made with the bucket tool