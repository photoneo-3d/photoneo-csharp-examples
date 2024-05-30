========================================================================
    CONSOLE APPLICATION : Recording Project Overview
========================================================================

This is a simple application that setups frame recording for connected device.

You will learn how to:

* Start and stop recording
* Check if recording is running
* Setup recording container for recording into desired (supported) format

The application will check if auto-enabled recording is configured and stop
it, setup options for recording into PLY container, trigger one frame and stop
recording. For other supported formats and available options, please see JSON
schema in: {PhoXiControl install directory}/API/RecordingOptionsSchema.json
If not connected to any scanner, it will automatically connect to the first
in PhoXiControl.

/////////////////////////////////////////////////////////////////////////////
