using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IService : IInitializable, IExecutable { }

public interface IInitializable {
    public void Initialize(ServiceInitParamWrapper initParams);
}
public interface IExecutable {
    public void Execute();
}


