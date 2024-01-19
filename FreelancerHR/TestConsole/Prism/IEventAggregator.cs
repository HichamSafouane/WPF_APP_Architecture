// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.PubSubEvents.IEventAggregator
// Assembly: Microsoft.Practices.Prism.PubSubEvents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 053C121F-9480-48F0-9AE5-39A0292B4BF8
// Assembly location: C:\MyData\Prism\CodePlex\compositewpf-ff6316df3dadd0d24083d51aa1dea922d07fb47e\V5\StockTrader RI\packages\Prism.PubSubEvents.1.0.0\lib\portable-sl4+wp7+windows8+net40\Microsoft.Practices.Prism.PubSubEvents.dll

namespace Microsoft.Practices.Prism.PubSubEvents
{
  public interface IEventAggregator
  {
    TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
  }
}
