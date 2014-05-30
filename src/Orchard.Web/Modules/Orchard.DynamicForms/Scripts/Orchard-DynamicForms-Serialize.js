//Update session storage for a new form
var saveLocal = function (formID) {
    var form = {
        DynamicFields: []
    };
    var html = $('.dynamicfield-dropped').html();
    var allDynamicFields = $('.dynamicfield-dropped');
    for (var i = 0; i < allDynamicFields.length; i++) {
        var dynamicfield = allDynamicFields[i];

        form.DynamicFields.push({
            Name: dynamicfield.viewModel.name,
            ClientId: dynamicfield.viewModel.clientId,
            //Id: dynamicfield.viewModel.clientId,
            State: dynamicfield.viewModel.state,
            DropZoneID: dynamicfield.viewModel.parentDropZoneID
        });
    }
   
    sessionStorage.setItem(formID, JSON.stringify(form));
};


//Remove the field from sessionStorage
var removeLocal = function (formID, fieldClientId) {
    var form = loadForm(formID);
    if (form == null) {
        return;
    }

    for (var i = 0; i < form.DynamicFields.length; i++) {
        var dynamicfield = form.DynamicFields[i];
        if (fieldClientId == dynamicfield.ClientId) {
            form.DynamicFields.splice(i, 1);
        }
    }

    sessionStorage.setItem(formID, JSON.stringify(form));
}


var loadDynamicFields = function (formID) {
    var form = loadForm(formID);
    if (form == null) {
        return;
    }

    // dynamicfields        
    for (var i = 0; i < form.DynamicFields.length; i++) {
        var dynamicfield = form.DynamicFields[i];
        zoneCount = dynamicfield.DropZoneID.slice(8);
        console.log("zoneCount: " + ++zoneCount);
        //If the drop zone does not exist, create it
        console.log("Dynamic Field Dropzone ID: " + dynamicfield.DropZoneID);
        $('#dynamicfield-editor').append("<div class='dropzones' id='" + dynamicfield.DropZoneID + "'></div><br />");

        renderField(dynamicfield.Id, dynamicfield.Name, dynamicfield.State, dynamicfield.DropZoneID);
    }
    return form;
};


//Update sessionStorage


//Load Form from session storage
var loadForm = function (formID) {
    var form = sessionStorage.getItem(formID);

    if (!form) {
        return;
    }
    // deserialize
    form = JSON.parse(form);
    return form;
};


var loadFieldForm = function (formID, ClientID) {
    var dynamicfield = getDynamicField(formID, ClientID);
    bindFieldForm($('form'), dynamicfield.State);
};

var getDynamicField = function (formID, ClientID) {

    var form = loadForm(formID);
    if (form == null) {
        return;
    }

    var dynamicfield = null;
    for (var i = 0; i < form.DynamicFields.length; i++) {
        var a = form.DynamicFields[i];

        if (a.ClientId == ClientID) {
            dynamicfield = a;
        }
    }

    return dynamicfield;

};

var bindFieldForm = function (form, data) {
    $.each(data, function (name, val) {
        var $el = $('[name="' + name + '"]'),
            tagName = $el.get(0).nodeName.toLowerCase(),
            type = $el.attr('type') && $el.attr('type').toLowerCase(),
            values = val.split(',');

        switch (tagName) {
            case 'input':
                switch (type) {
                    case 'checkbox':
                        $el.each(function () {
                            var self = $(this);
                            self.attr('checked', values.indexOf(self.attr('value')) != -1);
                            if (val == "on") {
                                document.getElementById("IsRequired").checked = true;
                            }
                        });
                        break;
                    case 'radio':
                        $el.filter('[value="' + val + '"]').attr('checked', 'checked');
                        break;
                    default:
                        $el.val(val);
                }
                break;
            case 'select':
                $el.val(values);

                break;
            default:
                $el.val(val);
        }
    });
};


//updating dynamic field properties

var updateDynamicFields = function (formID) {
    
    var form = loadForm(formID);
    if (form == null) {
        return;
    }

    if (updatedDynamicFieldState != null) {


        for (var i = 0; i < form.DynamicFields.length; i++) {
            var dynamicfield = form.DynamicFields[i];

            if (updatedDynamicFieldClientId == dynamicfield.ClientId) {
                dynamicfield.State = JSON.parse(updatedDynamicFieldState);
            }
        }
    }
    sessionStorage.setItem(formID, JSON.stringify(form));
};