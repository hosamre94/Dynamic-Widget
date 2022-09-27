import {setupWebpageUrlSelector} from "./webpage-url-selector";

let _isFirstTime;
let _cachedRows = {};

export function initiateDynamicWidget(){
    _isFirstTime = true;
    formSubmit();
    refreshUI();
    dragAndDrop();
}

function formSubmit() {
    $('table.dynamic-table').closest('form').on('submit', function () {
        let $form = $('<form>');
        $('[data-dynamic-input]').map((x, el) => {
            $('<input>').attr({
                type: 'hidden',
                name: el.name,
                value: el.type === 'checkbox' ? el.checked : el.value
            }).appendTo($form);
            //Maybe remove the element from the dom or keep it if the submit is faild for some reasons
            //$(el).remove()
        })

        let obj = $form.serializeJSON()
        $('<input>').attr({
            type: 'hidden',
            name: 'Properties',
            value: JSON.stringify(obj)
        }).appendTo($(this));
    })
}

function refreshUI() {
    $(".dynamic-table .add-row").each(function (i) {
        if (_isFirstTime) {
            const _row = $(this).closest('table').children('tbody').children('tr:nth-child(2)').clone();
            const _rowId = $(this).data('row-id');

            const _media = _row.find('.media-selector-initialized');
            _media.off();
            _media.removeClass('media-selector-initialized');
            _media.attr('id', 'm_temp')
            _media.closest('.form-group').html(_media);
            _row.find('input,select').val('');
            _cachedRows[_rowId] = _row;
        }

        //To make sure the event is not duplicated and we have a backup row if all rows are deleted
        $(this).off().click(function () {
            const _clone = _cachedRows[$(this).data('row-id')].clone();
            const _size = $(this).closest('table').children('tbody').children('tr').length;

            _clone.find('[data-dynamic-input]').each(function (){
                $(this).attr('id', $(this).attr('id') + '_' + _size )
            });

            //find a better way to integrate the custom inputs
            const _uniqueInputs = _clone.find('[data-unique-key]');
            if (_uniqueInputs.length > 0) {
                _uniqueInputs.each(function () {
                    const _id = "uk_" + Math.random().toString(36).substr(2, 5);
                    $(this).val(_id);
                    $(this).next().html(_id);
                })
            }
            _clone.find('[data-type=media-selector], [class=media-selector]').mediaSelector();
            _clone.find('td:first').html(_size);
            $(this).closest('table').append(_clone);
            setupWebpageUrlSelector();
            refreshUI();
        })
    })

    $(".dynamic-table .delete-row").off().click(function () {
        const didConfirm = confirm("Are you sure You want to delete");
        if (didConfirm === true) {
            $(this).closest('tr').remove();
        }
    });

    _isFirstTime = false;
}

function dragAndDrop() {
    if (document.querySelectorAll("#dynamic-widget-drag").length)
    {
        document.querySelector("#dynamic-widget-drag").ondragstart = function (e) {
            const dataTransfer = e.dataTransfer;
            dataTransfer.effectAllowed
            dataTransfer.setData("Text", e.target.dataset.text);
        }   
    }
}