namespace Tommy

open System.Runtime.InteropServices
open CSLib


///Importing dll written in C (CLib.dll)
module CLib = 
    [<DllImport("CLib.dll", CallingConvention=CallingConvention.Cdecl)>]
    extern void log(string);


///Main executable app
module App =

    let doStuff () = 
        use tommy = new Student("Tommy")
        tommy._logger.Level <- Logger.LogLevel.Fatal
     
    let mainWindowLoop () =
        use window = new Window (Factory.GetLogger ())
        while window.Running do
            window.OkButton.Click ()
            window.OkButton.Click ()
            window.OkButton.Click ()
            window.OkButton.Click ()
            window.ExitButton.Click ()
            window.OkButton.Click ()

    [<EntryPoint>]
    let Main (args: string[]): int =

        doStuff ()
    
        let logger: ILogger = Logger()
        logger.Warn "Starting..."
        args |> Seq.iter (fun a -> logger.Warn a)

        mainWindowLoop ()

        System.Console.Read() |> ignore
        
        0
        