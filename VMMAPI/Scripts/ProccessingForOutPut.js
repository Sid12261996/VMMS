


function ProccessingForOutPut(data) {
    //console.log(data);
    AssetNo = []; TransNo = []; var AssestId = 'AssestId', TransactionNo = 'TransactionNo'; Activity = 'Activity';
    dummy = []; AssetKit = {}; Obj = {};
    arr1 = data;
    arr1.forEach((item, index) => {
        if (dummy.includes(item[AssestId])) {

            var Trans = item[TransactionNo], Ass = item[AssestId];
            if (AssetKit[Ass + ' ' + Trans] == undefined) {
                AssetKit[Ass + ' ' + Trans] = new Array(item['Description'] + '*#*' + item[Activity]);
            }
            else {
                AssetKit[Ass + ' ' + Trans].push(item['Description'] + '*#*' + item[Activity]);
            }
        }
        else {
            dummy.push(item[AssestId]);


            var Trans = item[TransactionNo], Ass = item[AssestId];

            AssetKit[Ass + ' ' + Trans] = new Array(item['Description'] + '*#*' + item[Activity]);

        }
        Obj[item[TransactionNo]] = new Object(AssetKit);
    });

    function ReduceSingleCountArray(arr, TransIdForRef) {
        var dummy = [];

        if (arr != null || arr != undefined) {
            arr.filter((item) => {

                if (dummy.includes(item)) {

                } else {
                    dummy.push(item);
                }


            });
        }
        else { return; }
        return dummy;
    }

    function CompareTheString(arr, searchItem, SearchParameter) {
        if (SearchParameter) {

        } else {

            SearchParameter = " ";
        }

        if (arr.split(SearchParameter)[1] == searchItem) {

            return true;
        }
        else {

            return false;
        }
    }

    function ReduceSingleCountObject(arr, key, arranging = false) {
        if (key != undefined) { var transIdForRef = key; }


        if (arr != null || arr != undefined) {
            Object.keys(arr).filter((keyD) => {
                if (arranging) {
                    arr[keyD] = ArrMyData(arr[keyD]);
                }
                else {
                    arr[keyD] = ReduceSingleCountArray(arr[keyD], transIdForRef);
                }

            })

        }
        else { return; }

        //console.log(arr);
        return arr;
    }


    function ReducingObjectOfObjects(Obj, shaping, arrangingthearray) {
        Object.keys(Obj).filter((key) => {
            if (shaping) {

                Obj[key] = Shaping(Obj[key], key);

            } else {
                Obj[key] = ReduceSingleCountObject(Obj[key], key);
            }
            if (arrangingthearray) {
                Obj[key] = ReduceSingleCountObject(Obj[key], key, true);
            }

        });

        return Obj;

    }

    function ArrMyData(Obj) {
        var ob;


        ob = Obj.sort((a, b) => {
            //console.log(a.split('*#*')[1],b)
            a = a.split('*#*')[1];
            b = b.split('*#*')[1];
            return a - b;
        })


        return ob;
    }

    function Shaping(Obj, TransID) {
        var dummy = {};
        for (item in Obj) {

            if (CompareTheString(item, TransID)) {
                name = item.split(' ')[0];
                dummy[name] = Obj[item];
            }
        }

        return dummy;

    }
    function GetOtherProperties() {
        var o = {}; var Objj = {};
        Object.keys(Obj).filter((key) => {

            o = findtheProperties(key, Obj[key]);
            //console.log(o,'====')
            // console.log(Obj[key]);

            //console.log(Objj)
            Objj[key] = { ...Objj[key], ...o }
        });
        return Objj;

    }

    function AddingAssetType(TransId, AssetId) {
        RetObj = {}

        for (let id of AssetId) {
            AssetType = []

            arr1.filter((item, i) => {
                //console.log(TransId)
                if (item[TransactionNo] == TransId && item[AssestId] == id) {
                    AssetType.push(item['AssetsType']);

                }
            })
            RetObj[id] = AssetTypes;
        }
        //console.log(AssetType,"AsT");
        return RetObj;
    }

    function findtheProperties(key, a) {
        debugger;
        toReturn = {};
        //console.log(Object.keys(a),"K")
        arr1.find((item) => {
            if (item['TransactionNo'] == key) {


                toReturn["TransactionNo"] = item["TransactionNo"];
                toReturn["TaskName"] = item["TaskName"];
                toReturn["AssetsType"] = AddingAssetType(key, Object.keys(a));

                toReturn["CusId"] = item["CusId"];
                toReturn["AssignToNew"] = item["AssignToNew"];
                toReturn["CustName"] = item["CustName"];
                toReturn["Name"] = item["Name"];
                toReturn["State"] = item["State"];
                toReturn["City"] = item["City"];
                toReturn["DND"] = item["DND"];
                toReturn["ManagerAction"] = item["ManagerAction"];
                toReturn["TransactionNo1"] = item["TransactionNo1"];
                toReturn['Assets'] = a;
                toReturn["AcctualAssestID"] = item["AcctualAssestID"];
                toReturn["Activity"] = item["Activity"];
                toReturn["HostName"] = item["HostName"];
                toReturn["BImage"] = item["BImage"];
                toReturn["AImage"] = item["AImage"];
                toReturn["CompleteBy"] = item["CompleteBy"];
                toReturn["ActDate"] = item["ActDate"];
                toReturn["Description"] = item["Description"];
                toReturn["FaultyDesc"] = item["FaultyDesc"];
                toReturn["ManagerRemarks"] = item["ManagerRemarks"];
                toReturn["Status"] = item["Status"];
                toReturn["assestType"] = item["assestType"];
                //console.log(toReturn);

            }


        })
        return toReturn;
    }
    //debugger;
    Obj = ReducingObjectOfObjects(Obj, false);

    Obj = ReducingObjectOfObjects(Obj, true);
    Obj = ReducingObjectOfObjects(Obj, false, true);

    Obj = GetOtherProperties();
    console.log(Obj)
    return Obj;

}

function OnpopUp(element) {
    $("#" + element).modal();
}
function FindingNemo(arr, item) {
    return arr[item]; 

}

