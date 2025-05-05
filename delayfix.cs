void StartMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone found.");
            return;
        }

        micName = Microphone.devices[0];
        micClip = Microphone.Start(micName, true, 10, sampleRate);
        Debug.Log("Mic recording started: " + micName);

        StartCoroutine(SimpleMicLoop());
    }

    IEnumerator SimpleMicLoop()
    {
        while (true)
        {
            float[] chunk = new float[chunkSize];
            int micPos = Microphone.GetPosition(micName);
            int startPos = Mathf.Max(0, micPos - chunkSize);

            micClip.GetData(chunk, startPos);
            ProcessMicChunk(chunk);
            yield return new WaitForSeconds(0.5f);
        }
    }