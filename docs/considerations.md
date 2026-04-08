## Considerations
- Tried multithreading; performance was slower due to overhead from thread scheduling and task management.
    - This was expected, as the images are very small.
- Note: the signal function and counter could be combined by simply counting whenever the threshold is met and prev is 0 (prev tracked in the outer loop).
    - However, keeping them separate seemed cleaner architecturally. This allows other potential processes to use the signal independently and does not introduce significant overhead.