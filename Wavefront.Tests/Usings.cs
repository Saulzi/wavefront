global using NUnit.Framework;
global using Wavefront.AUV.API;
global using Wavefront.AUV.API.Interface;
global using Wavefront.AUV.API.Enums;
global using FakeItEasy;

/// This probably isn't the best place for this but it will do the job
/// This could and should probably be set on test by test basis, i.e. is required to test WPF related code
/// Which requires this threading model
[assembly: Apartment(ApartmentState.STA)]