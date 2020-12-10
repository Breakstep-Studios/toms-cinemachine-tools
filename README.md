# Toms Cinemachine Tools
A collection of tools for use with Unity's Cinemachine 

## Cinemachine Brain Helper
Adds a `onCameraBlendCompleteEvent` to the `CinemachineBrain` specified.

### Quick Start  

1. Add a `CinemachineBrainHelper` component to a GameObject.
2. Drag & drag the `CinemachineBrain` you want to recieve `onCameraBlendCompleteEvent` events for in to the `CinemachineBrainHelper.cinemachineBrain`field.
3. Hook in to the event by either utilizing the editor (Similiar to `CinemachineBrain` events) or in code using `onCameraBlendCompleteEvent.AddListener`.

## Simplified Cinemachine Brain Helper

Simplifies events coming from both `Cinemachinebrain` & `CinemachineBrainHelper` by only passing along one camera instead of two.

**Events Included**

* `onCameraBlendToCompleteEvent(blendedToCamera: ICinemachineCamera)`
* `onCameraBlendFromCompleteEvent(blendedFromCamera: ICinemachineCamera)`
* `onCameraActivatedToEvent(activatedToCamera: ICinemachineCamera)`
* `onCameraActivatedFromEvent(activatedFromCamera: ICinemachineCamera)`

### Quick Start  

See *Cinemachine Brain Helper* Quick Start. 
