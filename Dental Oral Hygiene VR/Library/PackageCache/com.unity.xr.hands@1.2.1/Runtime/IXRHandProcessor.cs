namespace UnityEngine.XR.Hands.Processing
{
    /// <summary>
    /// Derive from this interface with a <c>class</c> to intercept and alter
    /// hand and joint data. Register your processor with
    /// <see cref="XRHandSubsystem.RegisterProcessor"/>.
    /// </summary>
    public interface IXRHandProcessor
    {
        /// <summary>
        /// Returns the relative callback order for the
        /// <see cref="ProcessJoints"/> callback. Callbacks with lower values
        /// are called before callbacks with higher values.
        /// </summary>
        int callbackOrder { get; }

        /// <summary>
        /// Called after <see cref="XRHandSubsystem.preprocessJoints"/> is
        /// invoked and before <see cref="XRHandSubsystem.updatedHands"/> is
        /// invoked. Use extension methods to <see cref="XRHandSubsystem"/>,
        /// <see cref="XRHand"/>, and <see cref="XRHandJoint"/> in
        /// <see cref="XRHandProcessingUtility"/> to modify hand and joint data.
        /// </summary>
        void ProcessJoints(XRHandSubsystem subsystem, XRHandSubsystem.UpdateSuccessFlags successFlags, XRHandSubsystem.UpdateType updateType);
    }
}
