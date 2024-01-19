// Decompiled with JetBrains decompiler
// Type: PerkinElmer.Mmd.InstrumentConfiguration
// Assembly: EnSpireDataTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 33726249-DACB-4787-978F-CA09B77A1BF4
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\EnSpireDataTypes.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace PerkinElmer.Mmd
{
  [DataContract]
  [Serializable]
  public class InstrumentConfiguration : INotifyPropertyChanged
  {
    private Dictionary<string, Dictionary<InstrumentConfiguration.enumModul, bool>> _dicOperationConfig = (Dictionary<string, Dictionary<InstrumentConfiguration.enumModul, bool>>) null;
    private bool _SimulationMode = false;
    private int[] _FilterBarcodes = new int[0];
    private ExcitionLEDList ledAssembly = new ExcitionLEDList();
    private AutoFocusSettings autoFocusParams = new AutoFocusSettings();
    private int _SerialNumber;
    private bool _MainBoard;
    private bool _XYTrack;
    private bool _XYTrackYMover;
    private bool _LCPPlateDoor;
    private bool _MeasHeadMover;
    private bool _FlashUnit;
    private bool _AlphaUnit;
    private bool _EmsMonoChromator;
    private bool _ExcMonoChromator;
    private bool _MonoChromatorDetector;
    private bool _DispenserAddOns;
    private bool _FirstDispenser;
    private bool _SecondDispenser;
    private bool _LeftStacker;
    private bool _RightStacker;
    private bool _DispenserHead;
    private bool _LabelFree;
    private bool _Imaging;
    private bool _DichroChanger;
    private bool _PlateDoorII;
    private bool _StatusBar;
    private bool _Lightswitch;
    private bool _MeasHeadMover3M;
    private bool _TRFDetector;
    private bool _BarCodeReaderFront;
    private bool _BarCodeReaderBack;
    private bool _BarCodeReaderLeft;
    private bool _BarCodeReaderRight;

    [DataMember]
    public Dictionary<string, Dictionary<InstrumentConfiguration.enumModul, bool>> OperationConfig
    {
      get
      {
        return this._dicOperationConfig;
      }
      set
      {
        if (this._dicOperationConfig == value)
          return;
        this._dicOperationConfig = value;
        this.OnPropertyChanged("OperationConfig");
      }
    }

    [DataMember]
    public bool SimulationMode
    {
      get
      {
        return this._SimulationMode;
      }
      private set
      {
        if (this._SimulationMode == value)
          return;
        this._SimulationMode = value;
        this.OnPropertyChanged("SimulationMode");
      }
    }

    [DataMember]
    public int SerialNumber
    {
      get
      {
        return this._SerialNumber;
      }
      set
      {
        if (this._SerialNumber == value)
          return;
        this._SerialNumber = value;
        this.OnPropertyChanged("SerialNumber");
      }
    }

    [DataMember]
    public bool MainBoard
    {
      get
      {
        return this._MainBoard;
      }
      set
      {
        if (this._MainBoard == value)
          return;
        this._MainBoard = value;
        this.OnPropertyChanged("MainBoard");
      }
    }

    [DataMember]
    public bool XYTrack
    {
      get
      {
        return this._XYTrack;
      }
      set
      {
        if (this._XYTrack == value)
          return;
        this._XYTrack = value;
        this.OnPropertyChanged("XYTrack");
      }
    }

    [DataMember]
    public bool XYTrackYMover
    {
      get
      {
        return this._XYTrackYMover;
      }
      set
      {
        if (this._XYTrackYMover == value)
          return;
        this._XYTrackYMover = value;
        this.OnPropertyChanged("XYTrackYMover");
      }
    }

    [DataMember]
    public bool LCPPlateDoor
    {
      get
      {
        return this._LCPPlateDoor;
      }
      set
      {
        if (this._LCPPlateDoor == value)
          return;
        this._LCPPlateDoor = value;
        this.OnPropertyChanged("LCPPlateDoor");
      }
    }

    [DataMember]
    public bool MeasHeadMover
    {
      get
      {
        return this._MeasHeadMover;
      }
      set
      {
        if (this._MeasHeadMover == value)
          return;
        this._MeasHeadMover = value;
        this.OnPropertyChanged("MeasHeadMover");
      }
    }

    [DataMember]
    public bool FlashUnit
    {
      get
      {
        return this._FlashUnit;
      }
      set
      {
        if (this._FlashUnit == value)
          return;
        this._FlashUnit = value;
        this.OnPropertyChanged("FlashUnit");
      }
    }

    [DataMember]
    public bool AlphaUnit
    {
      get
      {
        return this._AlphaUnit;
      }
      set
      {
        if (this._AlphaUnit == value)
          return;
        this._AlphaUnit = value;
        this.OnPropertyChanged("FlashUnit");
      }
    }

    [DataMember]
    public bool EmsMonoChromator
    {
      get
      {
        return this._EmsMonoChromator;
      }
      set
      {
        if (this._EmsMonoChromator == value)
          return;
        this._EmsMonoChromator = value;
        this.OnPropertyChanged("EmsMonoChromator");
      }
    }

    [DataMember]
    public bool ExcMonoChromator
    {
      get
      {
        return this._ExcMonoChromator;
      }
      set
      {
        if (this._ExcMonoChromator == value)
          return;
        this._ExcMonoChromator = value;
        this.OnPropertyChanged("ExcMonoChromator");
      }
    }

    [DataMember]
    public bool MonoChromatorDetector
    {
      get
      {
        return this._MonoChromatorDetector;
      }
      set
      {
        if (this._MonoChromatorDetector == value)
          return;
        this._MonoChromatorDetector = value;
        this.OnPropertyChanged("MonoChromatorDetector");
      }
    }

    [DataMember]
    public bool DispenserAddOns
    {
      get
      {
        return this._DispenserAddOns;
      }
      set
      {
        if (this._DispenserAddOns == value)
          return;
        this._DispenserAddOns = value;
        this.OnPropertyChanged("DispenserAddOns");
      }
    }

    [DataMember]
    public bool FirstDispenser
    {
      get
      {
        return this._FirstDispenser;
      }
      set
      {
        if (this._FirstDispenser == value)
          return;
        this._FirstDispenser = value;
        this.OnPropertyChanged("FirstDispenser");
      }
    }

    [DataMember]
    public bool SecondDispenser
    {
      get
      {
        return this._SecondDispenser;
      }
      set
      {
        if (this._SecondDispenser == value)
          return;
        this._SecondDispenser = value;
        this.OnPropertyChanged("SecondDispenser");
      }
    }

    [DataMember]
    public bool LeftStacker
    {
      get
      {
        return this._LeftStacker;
      }
      set
      {
        if (this._LeftStacker == value)
          return;
        this._LeftStacker = value;
        this.OnPropertyChanged("LeftStacker");
      }
    }

    [DataMember]
    public bool RightStacker
    {
      get
      {
        return this._RightStacker;
      }
      set
      {
        if (this._RightStacker == value)
          return;
        this._RightStacker = value;
        this.OnPropertyChanged("RightStacker");
      }
    }

    [DataMember]
    public bool DispenserHead
    {
      get
      {
        return this._DispenserHead;
      }
      set
      {
        if (this._DispenserHead == value)
          return;
        this._DispenserHead = value;
        this.OnPropertyChanged("DispenserHead");
      }
    }

    [DataMember]
    public bool LabelFree
    {
      get
      {
        return this._LabelFree;
      }
      set
      {
        if (this._LabelFree == value)
          return;
        this._LabelFree = value;
        this.OnPropertyChanged("LabelFree");
      }
    }

    [DataMember]
    public bool Imaging
    {
      get
      {
        return this._Imaging;
      }
      set
      {
        if (this._Imaging == value)
          return;
        this._Imaging = value;
        this.OnPropertyChanged("Imaging");
      }
    }

    [DataMember]
    public bool DichroChanger
    {
      get
      {
        return this._DichroChanger;
      }
      set
      {
        if (this._DichroChanger == value)
          return;
        this._DichroChanger = value;
        this.OnPropertyChanged("DichroChanger");
      }
    }

    [DataMember]
    public bool PlateDoorII
    {
      get
      {
        return this._PlateDoorII;
      }
      set
      {
        if (this._PlateDoorII == value)
          return;
        this._PlateDoorII = value;
        this.OnPropertyChanged("PlateDoorII");
      }
    }

    [DataMember]
    public bool StatusBar
    {
      get
      {
        return this._StatusBar;
      }
      set
      {
        if (this._StatusBar == value)
          return;
        this._StatusBar = value;
        this.OnPropertyChanged("StatusBar");
      }
    }

    [DataMember]
    public bool Lightswitch
    {
      get
      {
        return this._Lightswitch;
      }
      set
      {
        if (this._Lightswitch == value)
          return;
        this._Lightswitch = value;
        this.OnPropertyChanged("Lightswitch");
      }
    }

    [DataMember]
    public bool MeasHeadMover3M
    {
      get
      {
        return this._MeasHeadMover3M;
      }
      set
      {
        if (this._MeasHeadMover3M == value)
          return;
        this._MeasHeadMover3M = value;
        this.OnPropertyChanged("MeasHeadMover3M");
      }
    }

    [DataMember]
    public int[] FilterBarcodes
    {
      get
      {
        return this._FilterBarcodes;
      }
      set
      {
        if (this._FilterBarcodes == value)
          return;
        this._FilterBarcodes = value;
        this.OnPropertyChanged("FilterBarcodes");
      }
    }

    [DataMember]
    public ExcitionLEDList LedAssembly
    {
      get
      {
        return this.ledAssembly;
      }
      set
      {
        if (this.ledAssembly == value)
          return;
        this.ledAssembly = value;
        this.OnPropertyChanged("LedAssembly");
      }
    }

    [DataMember]
    public AutoFocusSettings AutoFocusParams
    {
      get
      {
        return this.autoFocusParams;
      }
      set
      {
        bool flag = false;
        if (this.autoFocusParams.AF_Current != value.AF_Current)
        {
          this.autoFocusParams.AF_Current = value.AF_Current;
          flag = true;
        }
        if (this.autoFocusParams.AF_ExcitationTime_1 != value.AF_ExcitationTime_1)
        {
          this.autoFocusParams.AF_ExcitationTime_1 = value.AF_ExcitationTime_1;
          flag = true;
        }
        if (this.autoFocusParams.AF_ExcitationTime_2 != value.AF_ExcitationTime_2)
        {
          this.autoFocusParams.AF_ExcitationTime_2 = value.AF_ExcitationTime_2;
          flag = true;
        }
        if (!flag)
          return;
        this.OnPropertyChanged("AutoFocusSettings");
      }
    }

    [DataMember]
    public bool TRFDetector
    {
      get
      {
        return this._TRFDetector;
      }
      set
      {
        if (this._TRFDetector == value)
          return;
        this._TRFDetector = value;
        this.OnPropertyChanged("TRFDetector");
      }
    }

    [DataMember]
    public bool BarCodeReaderFront
    {
      get
      {
        return this._BarCodeReaderFront;
      }
      set
      {
        if (this._BarCodeReaderFront == value)
          return;
        this._BarCodeReaderFront = value;
        this.OnPropertyChanged("BarCodeReaderFront");
      }
    }

    [DataMember]
    public bool BarCodeReaderBack
    {
      get
      {
        return this._BarCodeReaderBack;
      }
      set
      {
        if (this._BarCodeReaderBack == value)
          return;
        this._BarCodeReaderBack = value;
        this.OnPropertyChanged("BarCodeReaderBack");
      }
    }

    [DataMember]
    public bool BarCodeReaderLeft
    {
      get
      {
        return this._BarCodeReaderLeft;
      }
      set
      {
        if (this._BarCodeReaderLeft == value)
          return;
        this._BarCodeReaderLeft = value;
        this.OnPropertyChanged("BarCodeReaderLeft");
      }
    }

    [DataMember]
    public bool BarCodeReaderRight
    {
      get
      {
        return this._BarCodeReaderRight;
      }
      set
      {
        if (this._BarCodeReaderRight == value)
          return;
        this._BarCodeReaderRight = value;
        this.OnPropertyChanged("BarCodeReaderRight");
      }
    }

    public ICollection<string> SupportedOperations
    {
      get
      {
        List<string> list = new List<string>();
        foreach (Type type in (IEnumerable<Type>) Operation.KnownOperations)
        {
          Operation operation = (Operation) Activator.CreateInstance(type);
          if (this.IsOperationSupportedByInstrument(this.OperationConfig[operation.TechnologyName]) || this._SimulationMode)
            list.Add(operation.TechnologyAbreviation);
        }
        return (ICollection<string>) list;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public InstrumentConfiguration()
    {
      this.GenerateOperationDictionary();
    }

    public InstrumentConfiguration(int csSerienNumber = 0, bool bSim = false)
    {
      this.GenerateOperationDictionary();
      this.SerialNumber = csSerienNumber;
      this._SimulationMode = bSim;
    }

    public InstrumentConfiguration(bool bXYTrack, bool bXYTrackYMover, bool bMainBoard, bool bLCPPlateDoor, bool bPlateDoorII, bool bMeasHeadMover, bool bMeasHeadMover3M, bool bFlashUnit, bool bEmsMonoChromator, bool bExcMonoChromator, bool bMonoChromatorDetector, bool bDispenserAddOns, bool bFirstDispenser, bool bSecondDispenser, bool bLeftStacker, bool bRightStacker, bool bDispenserHead, bool bLabelFree, bool bImaging, bool bDichroChanger, bool bStatusBar, bool bLightswitch, bool bAlphaUnit, int csSerienNumber = 0, bool bSim = false)
    {
      this.XYTrack = bXYTrack;
      this.XYTrackYMover = bXYTrackYMover;
      this.MainBoard = bMainBoard;
      this.LCPPlateDoor = bLCPPlateDoor;
      this.PlateDoorII = bPlateDoorII;
      this.MeasHeadMover = bMeasHeadMover;
      this.MeasHeadMover3M = bMeasHeadMover3M;
      this.FlashUnit = bFlashUnit;
      this.EmsMonoChromator = bEmsMonoChromator;
      this.ExcMonoChromator = bExcMonoChromator;
      this.MonoChromatorDetector = bMonoChromatorDetector;
      this.DispenserAddOns = bDispenserAddOns;
      this.FirstDispenser = bFirstDispenser;
      this.SecondDispenser = bSecondDispenser;
      this.LeftStacker = bLeftStacker;
      this.RightStacker = bRightStacker;
      this.DispenserHead = bDispenserHead;
      this.LabelFree = bLabelFree;
      this.Imaging = bImaging;
      this.DichroChanger = bDichroChanger;
      this.StatusBar = bStatusBar;
      this.Lightswitch = bLightswitch;
      this.AlphaUnit = bAlphaUnit;
      this.GenerateOperationDictionary();
      this.SerialNumber = csSerienNumber;
      this._SimulationMode = bSim;
    }

    public InstrumentConfiguration(Dictionary<InstrumentConfiguration.enumModul, bool> dicModul, int csSerienNumber = 0, bool bSim = false)
    {
      this.XYTrack = dicModul[InstrumentConfiguration.enumModul.XYTrack];
      this.XYTrackYMover = dicModul[InstrumentConfiguration.enumModul.XYTrackYMover];
      this.MainBoard = dicModul[InstrumentConfiguration.enumModul.MainBoard];
      this.LCPPlateDoor = dicModul[InstrumentConfiguration.enumModul.LCPPlateDoor];
      this.PlateDoorII = dicModul[InstrumentConfiguration.enumModul.PlateDoorII];
      this.MeasHeadMover = dicModul[InstrumentConfiguration.enumModul.MeasHeadMover];
      this.MeasHeadMover3M = dicModul[InstrumentConfiguration.enumModul.MeasHeadMover3M];
      this.FlashUnit = dicModul[InstrumentConfiguration.enumModul.FlashUnit];
      this.EmsMonoChromator = dicModul[InstrumentConfiguration.enumModul.EmsMonoChromator];
      this.ExcMonoChromator = dicModul[InstrumentConfiguration.enumModul.ExcMonoChromator];
      this.MonoChromatorDetector = dicModul[InstrumentConfiguration.enumModul.MonoChromatorDetector];
      this.DispenserAddOns = dicModul[InstrumentConfiguration.enumModul.DispenserAddOns];
      this.FirstDispenser = dicModul[InstrumentConfiguration.enumModul.FirstDispenser];
      this.SecondDispenser = dicModul[InstrumentConfiguration.enumModul.SecondDispenser];
      this.LeftStacker = dicModul[InstrumentConfiguration.enumModul.LeftStacker];
      this.RightStacker = dicModul[InstrumentConfiguration.enumModul.RightStacker];
      this.DispenserHead = dicModul[InstrumentConfiguration.enumModul.DispenserHead];
      this.LabelFree = dicModul[InstrumentConfiguration.enumModul.LabelFree];
      this.Imaging = dicModul[InstrumentConfiguration.enumModul.Imaging];
      this.DichroChanger = dicModul[InstrumentConfiguration.enumModul.DichroChanger];
      this.StatusBar = dicModul[InstrumentConfiguration.enumModul.Statusbar];
      this.Lightswitch = dicModul[InstrumentConfiguration.enumModul.LightSwitch];
      this.AlphaUnit = dicModul[InstrumentConfiguration.enumModul.AlphaUnit];
      this.GenerateOperationDictionary();
      this.SerialNumber = csSerienNumber;
      this._SimulationMode = bSim;
    }

    protected void GenerateOperationDictionary()
    {
      this.OperationConfig = new Dictionary<string, Dictionary<InstrumentConfiguration.enumModul, bool>>();
      this.OperationConfig.Add(new ABSFOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
      this.OperationConfig.Add(new ABSMOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, true, false, false));
      this.OperationConfig.Add(new ASOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false));
      this.OperationConfig.Add(new FIOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, true, false, false));
      this.OperationConfig.Add(new ImagingOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false));
      this.OperationConfig.Add(new LFOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false));
      this.OperationConfig.Add(new LUMOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false));
      this.OperationConfig.Add(new TRFOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, true, false, true));
      this.OperationConfig.Add(new DispenseOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, true, true, true, false, false, true, false, false, false, false, false, false, false));
      this.OperationConfig.Add(new ShakeOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
      this.OperationConfig.Add(new WaitOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
      this.OperationConfig.Add(new TemperatureAdjustmentOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
      this.OperationConfig.Add(new GroupOperation().TechnologyName, this.SetOperation(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
      this.LedAssembly.PropertyChanged += new PropertyChangedEventHandler(this.LedAssembly_PropertyChanged);
    }

    protected Dictionary<InstrumentConfiguration.enumModul, bool> SetOperation(bool bXYTrack, bool bXYTrackYMover, bool bMainBoard, bool bLCPPlateDoor, bool bPlateDoorII, bool bMeasHeadMover, bool bMeasHeadMover3M, bool bFlashUnit, bool bEmsMonoChromator, bool bExcMonoChromator, bool bMonoChromatorDetector, bool bDispenserAddOns, bool bFirstDispenser, bool bSecondDispenser, bool bLeftStacker, bool bRightStacker, bool bDispenserHead, bool bLabelFree, bool bImaging, bool bDichroChanger, bool bStatusBar, bool bLightswitch, bool bAlphaUnit, bool bTRFDetector)
    {
      return new Dictionary<InstrumentConfiguration.enumModul, bool>()
      {
        {
          InstrumentConfiguration.enumModul.XYTrack,
          bXYTrack
        },
        {
          InstrumentConfiguration.enumModul.XYTrackYMover,
          bXYTrackYMover
        },
        {
          InstrumentConfiguration.enumModul.MainBoard,
          bMainBoard
        },
        {
          InstrumentConfiguration.enumModul.LCPPlateDoor,
          bLCPPlateDoor
        },
        {
          InstrumentConfiguration.enumModul.PlateDoorII,
          bPlateDoorII
        },
        {
          InstrumentConfiguration.enumModul.MeasHeadMover,
          bMeasHeadMover
        },
        {
          InstrumentConfiguration.enumModul.MeasHeadMover3M,
          bMeasHeadMover3M
        },
        {
          InstrumentConfiguration.enumModul.FlashUnit,
          bFlashUnit
        },
        {
          InstrumentConfiguration.enumModul.EmsMonoChromator,
          bEmsMonoChromator
        },
        {
          InstrumentConfiguration.enumModul.ExcMonoChromator,
          bExcMonoChromator
        },
        {
          InstrumentConfiguration.enumModul.MonoChromatorDetector,
          bMonoChromatorDetector
        },
        {
          InstrumentConfiguration.enumModul.DispenserAddOns,
          bDispenserAddOns
        },
        {
          InstrumentConfiguration.enumModul.FirstDispenser,
          bFirstDispenser
        },
        {
          InstrumentConfiguration.enumModul.SecondDispenser,
          bSecondDispenser
        },
        {
          InstrumentConfiguration.enumModul.LeftStacker,
          bLeftStacker
        },
        {
          InstrumentConfiguration.enumModul.RightStacker,
          bRightStacker
        },
        {
          InstrumentConfiguration.enumModul.DispenserHead,
          bDispenserHead
        },
        {
          InstrumentConfiguration.enumModul.LabelFree,
          bLabelFree
        },
        {
          InstrumentConfiguration.enumModul.Imaging,
          bImaging
        },
        {
          InstrumentConfiguration.enumModul.DichroChanger,
          bDichroChanger
        },
        {
          InstrumentConfiguration.enumModul.Statusbar,
          bStatusBar
        },
        {
          InstrumentConfiguration.enumModul.LightSwitch,
          bLightswitch
        },
        {
          InstrumentConfiguration.enumModul.AlphaUnit,
          bAlphaUnit
        },
        {
          InstrumentConfiguration.enumModul.TRFDetector,
          bTRFDetector
        }
      };
    }

    public bool IsOperationSupported(Operation op)
    {
      bool flag = false;
      if (this.SimulationMode)
        Console.WriteLine("SimulationMode");
      if (op != null)
      {
        if (op is GroupOperation)
        {
          flag = true;
          foreach (Operation op1 in (Collection<Operation>) ((GroupOperation) op).Operations)
            flag = flag && this.IsOperationSupported(op1);
        }
        else
          flag = (this.IsOperationSupportedByInstrument(this.OperationConfig[op.TechnologyName]) || this.SimulationMode) && this.IsOperationSupportedByCurrentEquipment(op);
      }
      return flag;
    }

    private bool IsOperationSupportedByCurrentEquipment(Operation operation)
    {
      return this.IsFilterInDevice(operation.NeededFilters) && this.IsLEDInDevice(operation);
    }

    private bool IsFilterInDevice(List<FilterBasedWavelengthDescriptor> filters)
    {
      bool flag1 = true;
      if (this.FilterBarcodes == null)
        return false;
      if (filters != null)
      {
        bool flag2 = true;
        foreach (FilterBasedWavelengthDescriptor wavelengthDescriptor in filters)
        {
          flag1 = false;
          foreach (int num in this.FilterBarcodes)
          {
            if (wavelengthDescriptor.Filter != null && wavelengthDescriptor.Filter.IsAvailable)
              flag1 = true;
            if (wavelengthDescriptor.Filter != null && num == wavelengthDescriptor.Filter.ID)
            {
              flag1 = true;
              break;
            }
          }
          flag2 = flag2 && flag1;
        }
      }
      return flag1;
    }

    private bool IsLEDInDevice(Operation operation)
    {
      bool flag = true;
      if (operation is ImagingOperation)
      {
        ImagingOperation imagingOperation = operation as ImagingOperation;
        if (this.Imaging)
          ;
        if (imagingOperation.Channels.Count > 0 && flag && this.LedAssembly != null && this.LedAssembly.Leds != null)
        {
          flag = false;
          foreach (ImagingChannel imagingChannel in (Collection<ImagingChannel>) imagingOperation.Channels)
          {
            if (imagingChannel.WavelengthDescriptor is ExcitationClassificatonBasedWavelengthDescriptor)
            {
              ExcitationClassificatonBasedWavelengthDescriptor wavelengthDescriptor = imagingChannel.WavelengthDescriptor as ExcitationClassificatonBasedWavelengthDescriptor;
              int num;
              if (wavelengthDescriptor != null && wavelengthDescriptor.ExcitationClassification != null)
              {
                ExcitionLEDList ledAssembly = this.LedAssembly;
                short? nullable = wavelengthDescriptor.ExcitationClassification.WavelengthMin_nm;
                int WavelengthMin = (int) nullable.Value;
                nullable = wavelengthDescriptor.ExcitationClassification.WavelengthMax_nm;
                int WavelengthMax = (int) nullable.Value;
                bool? transmittedLight = wavelengthDescriptor.ExcitationClassification.IsTransmittedLight;
                num = ledAssembly.GetLED(WavelengthMin, WavelengthMax, transmittedLight) == null ? 1 : 0;
              }
              else
                num = 1;
              if (num == 0)
                flag = true;
            }
            if (imagingChannel.WavelengthDescriptor is LEDBasedWavelengthDescriptor)
            {
              LEDBasedWavelengthDescriptor ec = imagingChannel.WavelengthDescriptor as LEDBasedWavelengthDescriptor;
              if (ec != null && Enumerable.FirstOrDefault<ExcitionLED>(Enumerable.Where<ExcitionLED>((IEnumerable<ExcitionLED>) this.LedAssembly.Leds, (Func<ExcitionLED, bool>) (n => n.LedSlot == ec.LEDSlot))) != null)
                flag = true;
            }
          }
        }
      }
      return flag;
    }

    private bool IsOperationSupportedByInstrument(Dictionary<InstrumentConfiguration.enumModul, bool> dicOperation)
    {
      return dicOperation != null && (this.XYTrack || dicOperation[InstrumentConfiguration.enumModul.XYTrack] == this.XYTrack) && ((this.XYTrackYMover || dicOperation[InstrumentConfiguration.enumModul.XYTrackYMover] == this.XYTrackYMover) && (this.MainBoard || dicOperation[InstrumentConfiguration.enumModul.MainBoard] == this.MainBoard)) && ((this.LCPPlateDoor || dicOperation[InstrumentConfiguration.enumModul.LCPPlateDoor] == this.LCPPlateDoor || (this.PlateDoorII || dicOperation[InstrumentConfiguration.enumModul.PlateDoorII] == this.PlateDoorII)) && (this.MeasHeadMover || dicOperation[InstrumentConfiguration.enumModul.MeasHeadMover] == this.MeasHeadMover || (this.MeasHeadMover3M || dicOperation[InstrumentConfiguration.enumModul.MeasHeadMover3M] == this.MeasHeadMover3M))) && ((this.FlashUnit || dicOperation[InstrumentConfiguration.enumModul.FlashUnit] == this.FlashUnit) && (this.EmsMonoChromator || dicOperation[InstrumentConfiguration.enumModul.EmsMonoChromator] == this.EmsMonoChromator) && ((this.ExcMonoChromator || dicOperation[InstrumentConfiguration.enumModul.ExcMonoChromator] == this.ExcMonoChromator) && (this.MonoChromatorDetector || dicOperation[InstrumentConfiguration.enumModul.MonoChromatorDetector] == this.MonoChromatorDetector)) && ((this.DispenserAddOns || dicOperation[InstrumentConfiguration.enumModul.DispenserAddOns] == this.DispenserAddOns) && (this.FirstDispenser || dicOperation[InstrumentConfiguration.enumModul.FirstDispenser] == this.FirstDispenser) && ((this.SecondDispenser || dicOperation[InstrumentConfiguration.enumModul.SecondDispenser] == this.SecondDispenser) && (this.LeftStacker || dicOperation[InstrumentConfiguration.enumModul.LeftStacker] == this.LeftStacker)))) && ((this.RightStacker || dicOperation[InstrumentConfiguration.enumModul.RightStacker] == this.RightStacker) && (this.DispenserHead || dicOperation[InstrumentConfiguration.enumModul.DispenserHead] == this.DispenserHead) && ((this.LabelFree || dicOperation[InstrumentConfiguration.enumModul.LabelFree] == this.LabelFree) && (this.Imaging || dicOperation[InstrumentConfiguration.enumModul.Imaging] == this.Imaging)) && ((this.DichroChanger || dicOperation[InstrumentConfiguration.enumModul.DichroChanger] == this.DichroChanger) && (this.StatusBar || dicOperation[InstrumentConfiguration.enumModul.Statusbar] == this.StatusBar) && ((this.Lightswitch || dicOperation[InstrumentConfiguration.enumModul.LightSwitch] == this.Lightswitch) && (this.AlphaUnit || dicOperation[InstrumentConfiguration.enumModul.AlphaUnit] == this.AlphaUnit)))) && (this.TRFDetector || dicOperation[InstrumentConfiguration.enumModul.TRFDetector] == this.TRFDetector);
    }

    private void PrintCheck(Dictionary<InstrumentConfiguration.enumModul, bool> dicOperation, Operation op)
    {
      Console.WriteLine(op.ToString() + " Needed Devices:");
      foreach (KeyValuePair<InstrumentConfiguration.enumModul, bool> keyValuePair in dicOperation)
        Console.WriteLine(keyValuePair.ToString());
      Console.WriteLine("XYTrack: " + (object) (bool) (this.XYTrack ? 1 : 0));
      Console.WriteLine("XYTrackYMover: " + (object) (bool) (this.XYTrackYMover ? 1 : 0));
      Console.WriteLine("MainBoard: " + (object) (bool) (this.MainBoard ? 1 : 0));
      Console.WriteLine("PlateDoor: " + (object) (bool) (this.LCPPlateDoor ? 1 : 0) + (string) (object) (bool) (this.PlateDoorII ? 1 : 0));
      Console.WriteLine("MeasHeadMover: " + (object) (bool) (this.MeasHeadMover ? 1 : 0) + (string) (object) (bool) (this.MeasHeadMover3M ? 1 : 0));
      Console.WriteLine("FlashUnit: " + (object) (bool) (this.FlashUnit ? 1 : 0));
      Console.WriteLine("EmsMonoChromator: " + (object) (bool) (this.EmsMonoChromator ? 1 : 0));
      Console.WriteLine("ExcMonoChromator: " + (object) (bool) (this.ExcMonoChromator ? 1 : 0));
      Console.WriteLine("MonoChromatorDetector: " + (object) (bool) (this.MonoChromatorDetector ? 1 : 0));
      Console.WriteLine("DispenserAddOns: " + (object) (bool) (this.DispenserAddOns ? 1 : 0));
      Console.WriteLine("FirstDispenser: " + (object) (bool) (this.FirstDispenser ? 1 : 0));
      Console.WriteLine("SecondDispenser: " + (object) (bool) (this.SecondDispenser ? 1 : 0));
      Console.WriteLine("LeftStacker: " + (object) (bool) (this.LeftStacker ? 1 : 0));
      Console.WriteLine("RightStacker: " + (object) (bool) (this.RightStacker ? 1 : 0));
      Console.WriteLine("DispenserHead: " + (object) (bool) (this.DispenserHead ? 1 : 0));
      Console.WriteLine("LabelFree: " + (object) (bool) (this.LabelFree ? 1 : 0));
      Console.WriteLine("Imaging: " + (object) (bool) (this.Imaging ? 1 : 0));
      Console.WriteLine("DichroChanger: " + (object) (bool) (this.DichroChanger ? 1 : 0));
      Console.WriteLine("Statusbar: " + (object) (bool) (this.StatusBar ? 1 : 0));
      Console.WriteLine("LigthSwitch: " + (object) (bool) (this.Lightswitch ? 1 : 0));
      Console.WriteLine("AlphaUnit: " + (object) (bool) (this.AlphaUnit ? 1 : 0));
      Console.WriteLine("XYTrack: " + (this.XYTrack || dicOperation[InstrumentConfiguration.enumModul.XYTrack] == this.XYTrack).ToString());
      Console.WriteLine("XYTrackYMover: " + (this.XYTrackYMover || dicOperation[InstrumentConfiguration.enumModul.XYTrackYMover] == this.XYTrackYMover).ToString());
      Console.WriteLine("MainBoard: " + (this.MainBoard || dicOperation[InstrumentConfiguration.enumModul.MainBoard] == this.MainBoard).ToString());
      Console.WriteLine("PlateDoor: " + (this.LCPPlateDoor || dicOperation[InstrumentConfiguration.enumModul.LCPPlateDoor] == this.LCPPlateDoor || (this.PlateDoorII || dicOperation[InstrumentConfiguration.enumModul.PlateDoorII] == this.PlateDoorII)).ToString());
      Console.WriteLine("MeasHeadMover: " + (this.MeasHeadMover || dicOperation[InstrumentConfiguration.enumModul.MeasHeadMover] == this.MeasHeadMover || (this.MeasHeadMover3M || dicOperation[InstrumentConfiguration.enumModul.MeasHeadMover3M] == this.MeasHeadMover3M)).ToString());
      Console.WriteLine("FlashUnit: " + (this.FlashUnit || dicOperation[InstrumentConfiguration.enumModul.FlashUnit] == this.FlashUnit).ToString());
      Console.WriteLine("EmsMonoChromator: " + (this.EmsMonoChromator || dicOperation[InstrumentConfiguration.enumModul.EmsMonoChromator] == this.EmsMonoChromator).ToString());
      Console.WriteLine("ExcMonoChromator: " + (this.ExcMonoChromator || dicOperation[InstrumentConfiguration.enumModul.ExcMonoChromator] == this.ExcMonoChromator).ToString());
      Console.WriteLine("MonoChromatorDetector: " + (this.MonoChromatorDetector || dicOperation[InstrumentConfiguration.enumModul.MonoChromatorDetector] == this.MonoChromatorDetector).ToString());
      Console.WriteLine("DispenserAddOns: " + (this.DispenserAddOns || dicOperation[InstrumentConfiguration.enumModul.DispenserAddOns] == this.DispenserAddOns).ToString());
      Console.WriteLine("FirstDispenser: " + (this.FirstDispenser || dicOperation[InstrumentConfiguration.enumModul.FirstDispenser] == this.FirstDispenser).ToString());
      Console.WriteLine("SecondDispenser: " + (this.SecondDispenser || dicOperation[InstrumentConfiguration.enumModul.SecondDispenser] == this.SecondDispenser).ToString());
      Console.WriteLine("LeftStacker: " + (this.LeftStacker || dicOperation[InstrumentConfiguration.enumModul.LeftStacker] == this.LeftStacker).ToString());
      Console.WriteLine("RightStacker: " + (this.RightStacker || dicOperation[InstrumentConfiguration.enumModul.RightStacker] == this.RightStacker).ToString());
      Console.WriteLine("DispenserHead: " + (this.DispenserHead || dicOperation[InstrumentConfiguration.enumModul.DispenserHead] == this.DispenserHead).ToString());
      Console.WriteLine("LabelFree: " + (this.LabelFree || dicOperation[InstrumentConfiguration.enumModul.LabelFree] == this.LabelFree).ToString());
      Console.WriteLine("Imaging: " + (this.Imaging || dicOperation[InstrumentConfiguration.enumModul.Imaging] == this.Imaging).ToString());
      Console.WriteLine("DichroChanger: " + (this.DichroChanger || dicOperation[InstrumentConfiguration.enumModul.DichroChanger] == this.DichroChanger).ToString());
      Console.WriteLine("Statusbar: " + (this.StatusBar || dicOperation[InstrumentConfiguration.enumModul.Statusbar] == this.StatusBar).ToString());
      Console.WriteLine("LigthSwitch: " + (this.Lightswitch || dicOperation[InstrumentConfiguration.enumModul.LightSwitch] == this.Lightswitch).ToString());
      Console.WriteLine("AlphaUnit: " + (this.AlphaUnit || dicOperation[InstrumentConfiguration.enumModul.AlphaUnit] == this.AlphaUnit).ToString());
    }

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    protected void LedAssembly_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged("InstrumentConfiguration");
    }

    [DataContract]
    [Flags]
    [Serializable]
    public enum enumModul
    {
      [EnumMember] XYTrack = 0,
      [EnumMember] XYTrackYMover = 1,
      [EnumMember] MainBoard = 2,
      [EnumMember] MeasHeadMover = MainBoard | XYTrackYMover,
      [EnumMember] MeasHeadMover3M = 4,
      [EnumMember] DispenserHead = MeasHeadMover3M | XYTrackYMover,
      [EnumMember] LCPPlateDoor = MeasHeadMover3M | MainBoard,
      [EnumMember] PlateDoorII = LCPPlateDoor | XYTrackYMover,
      [EnumMember] LeftStacker = 8,
      [EnumMember] RightStacker = LeftStacker | XYTrackYMover,
      [EnumMember] FirstDispenser = LeftStacker | MainBoard,
      [EnumMember] SecondDispenser = FirstDispenser | XYTrackYMover,
      [EnumMember] DispenserAddOns = LeftStacker | MeasHeadMover3M,
      [EnumMember] ExcMonoChromator = DispenserAddOns | XYTrackYMover,
      [EnumMember] EmsMonoChromator = DispenserAddOns | MainBoard,
      [EnumMember] MonoChromatorDetector = EmsMonoChromator | XYTrackYMover,
      [EnumMember] FlashUnit = 16,
      [EnumMember] AlphaUnit = FlashUnit | XYTrackYMover,
      [EnumMember] LabelFree = FlashUnit | MainBoard,
      [EnumMember] Imaging = LabelFree | XYTrackYMover,
      [EnumMember] DichroChanger = FlashUnit | MeasHeadMover3M,
      [EnumMember] Statusbar = DichroChanger | XYTrackYMover,
      [EnumMember] LightSwitch = DichroChanger | MainBoard,
      [EnumMember] TRFDetector = LightSwitch | XYTrackYMover,
    }
  }
}
