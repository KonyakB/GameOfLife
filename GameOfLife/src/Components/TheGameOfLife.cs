namespace GameOfLife.Components;

public class TheGameOfLife
{
    private AutomationSimulator _automationSimulator = new();
    
    public void Run()
    {
        _automationSimulator.InitializeGrid(5,5 );
        _automationSimulator.RunSimulation();
    }
}