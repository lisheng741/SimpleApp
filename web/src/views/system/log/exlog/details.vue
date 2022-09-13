<template>
  <a-modal
    title="日志详情"
    :width="900"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @cancel="handleCancel"
  >
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-row :gutter="24">
          <a-col :md="12" :sm="24">
            <a-form-item
              label="事件Id"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['eventId']" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24">
            <a-form-item
              label="异常名称"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['name']" />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :md="12" :sm="24">
            <a-form-item
              label="异常消息"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['message']" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24">
            <a-form-item
              label="异常源"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['exceptionSource']" />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :md="12" :sm="24">
            <a-form-item
              label="调用类"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['className']" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24">
            <a-form-item
              label="调用方法"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input v-decorator="['methodName']" />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :md="12" :sm="24">
            <a-form-item
              label="参数"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input :rows="4" v-decorator="['parameters']"/>
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24">
            <a-form-item
              label="异常时间"
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
            >
              <a-input :rows="4" v-decorator="['exceptionTime']"/>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :md="24" :sm="24">
            <a-form-item
              label="堆栈信息"
              :labelCol="{ span: 2 }"
              :wrapperCol="{ span: 21 }"
            >
              <a-textarea :rows="8" v-decorator="['stackTrace']"/>
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-spin>
  </a-modal>
</template>
<script>
  export default {
    data () {
      return {
        labelCol: {
          xs: { span: 24 },
          sm: { span: 4 }
        },
        wrapperCol: {
          xs: { span: 24 },
          sm: { span: 18 }
        },
        visible: false,
        confirmLoading: false,
        form: this.$form.createForm(this)
      }
    },
    methods: {
      // 初始化方法
      details (record) {
        this.visible = true
        setTimeout(() => {
          this.form.setFieldsValue(
            {
              eventId: record.eventId,
              name: record.name,
              message: record.message,
              exceptionSource: record.exceptionSource,
              className: record.className,
              methodName: record.methodName,
              parameters: record.parameters,
              exceptionTime: record.exceptionTime,
              stackTrace: record.stackTrace
            }
          )
        }, 100)
      },
      handleCancel () {
        this.form.resetFields()
        this.visible = false
      }
    }
  }
</script>
